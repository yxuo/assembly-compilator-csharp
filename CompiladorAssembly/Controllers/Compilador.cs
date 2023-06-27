using CompiladorAssembly.Models;
using CompiladorAssembly.Services;
using System.Data;
using System.Text.RegularExpressions;

namespace CompiladorAssembly.Controllers
{

    public class CompiladorDados
    {
        public Dictionary<string, string> Registradores { get; set; } = new();
        public List<string> Separadores { get; } = new List<string> { " ", ",", ":", "(", ")" };
        public IReadOnlyList<string> OperadoresMat { get; } = new List<string> { "=", "+", "-", "*", "/" };
        public IReadOnlyList<string> OperadoresLogic { get; } = new List<string> { "==", "<", ">", "!=", "&&" };
        public List<PalavraChave> PalavrasChave { get; set; } = new List<PalavraChave>
        {
            new PalavraChaveGenérica("FUNCTION", new List<TokenTipo> { TokenTipo.PalavraChave }),
            new PalavraChaveGenérica("VAR"),
            new PalavraChaveGenérica("RETURN"),
            new PalavraChaveGenérica("END"),
            new PalavraChaveGenérica("LER"),
            new PalavraChaveGenérica("ESCREVER"),
        };
        public List<string> GetPalavrasChaveNome()
        {
            return PalavrasChave.Select(p => p.Nome).ToList();
        }

        public string CR = "CR";

    }

    public class MapOperadores : Dictionary<int, Token> { }

    class Compilador
    {

        // Dados usados vários lugares desse programa
        public CompiladorDados Dados { get; set; } = new();

        // Dados usados apenas nessa classe
        public List<Instrução> ListaInstruções { get; set; } = new List<Instrução>();

        public string? Variavel { get; set; }
        public string Variavel2 { get; set; } = "";
        public string ÚltimaVarOperador { get; set; } = "";
        public string Teste { get; set; } = "";
        public bool IniciarLeitura { get; set; } = false;
        public List<string> VariaveisNome { get; set; } = new();
        public List<string> InstruçãoLinha { get; set; } = new List<string>();
        public List<dynamic>? MemóriaValor { get; set; } = new();
        public List<string> ÍndiceVariáveisLocais { get; set; } = new List<string>();

        public Dictionary<char, int> Variáveis { get; set; } = new();
        public string AssemblyText { get; set; } = "";

        /// <summary>
        /// A partir de um arquivo, executa a compilação.
        /// </summary>
        public void CompilarArquivo()
        {
            using StreamReader arquivo = new("./Teste.txt");

            GerarTokens(arquivo);
            // TODO: ValidarTokens
            ConverterAssembly();

        }

        public string Arquivo = @"
FUNCTION SOMAR1 : a, b
     VAR resultado
     resultado = a + b
     RETURN resultado
END
VAR x
VAR y
x = LER
y = LER
x = SOMAR1(x,y)

WHILE a == b && c == d
END_WHILE

            ";
        public void GerarTokens(StreamReader arquivo)
        {
            // esvaziar ListaInstruções
            ListaInstruções.Clear();

            // Para cada linha no arquivo, transforma em token
            // <Enquanto a leitura não estiver no final do arquivo>            
            foreach (string _linha in Arquivo.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None))
            // while (!arquivo.EndOfStream)
            {
                string linha = _linha;
                // string? linha = arquivo.ReadLine();

                // Se a linha for nula, pula para a próxima linha
                if (linha == null)
                {
                    continue;
                }


                // Remove espaço em branco no começo e no fim da linha
                // Exemplo: " print hello " -> "print hello"
                linha = linha.Trim();
                // Remover comentário
                linha = linha.Split("--")[0];
                if (linha.Length == 0)
                {
                    continue;
                }

                InstruçãoLinha.Add(linha);
                // 1 linha = 1 instrução
                // Converter instrução em string para instrrução em tokens
                Instrução instrução = ConversorToken.ConverterInstrução(linha, Dados);

                // Debug
                Console.WriteLine(linha);
                Console.WriteLine(instrução + "\n");
                Console.WriteLine("---------");


                // Adicionar instrução em ListaInstruções
                ListaInstruções.Add(instrução);
            }
        }

        public void ConverterAssembly()
        {
            ConverterParaAssembly(ListaInstruções);
            Console.WriteLine(AssemblyText);
            System.Console.WriteLine(Teste);
        }

        public void ConverterParaAssembly(List<Instrução> listaInstrução)
        {
            // VAR(listaInstrução);
            LER(listaInstrução);
            FUNCTION();
            foreach (Instrução instrução in listaInstrução)
            {
                // Operador(instrução);
                // BOOL(instrução);
            }
        }

        public void LER(List<Instrução> instrução)
        {
            // VariaveisNome = new List<string>();
            // VariaveisValor = new List<dynamic>();
            string nome = "LER";
            bool função = true;
            foreach (Instrução item in instrução)
            {
                for (int i = 0; i < item.Count(); i++)
                {
                    string valor = item[i].Valor;
                    if (valor == "END")
                    {
                        função = false;
                    }
                    if (!função)
                    {
                        if (valor == "VAR")
                        {
                            VariaveisNome.Add(item[i + 1].Valor);
                        }
                        else if (valor == nome)
                        {
                            INT_1();
                        }
                    }
                }
            }
            for (int j = 0; j < VariaveisNome.Count(); j++)
            {
                AssemblyText += $"MOVE {VariaveisNome[j]}, {MemóriaValor[j]} \n";
            }
        }
        public void INT_1()
        {
            Console.WriteLine("Digite o número: ");
            Variavel = Console.ReadLine();
            MemóriaValor.Add(Variavel);
            AssemblyText += $"INT 1, {Variavel} \n";
        }
        public void FUNCTION()
        {
            for (int i = 0; i < InstruçãoLinha.Count(); i++)
            {
                if (InstruçãoLinha[i].Contains("FUNCTION"))
                {
                    IniciarLeitura = true;
                }
                if (IniciarLeitura)
                {
                    if (InstruçãoLinha[i].Contains("VAR"))
                    {
                        //Dados.Operadores
                        Variavel2 = InstruçãoLinha[i].Split(' ')[1];
                    }
                    if (!InstruçãoLinha[i].Contains("VAR") && InstruçãoLinha[i].Contains(Variavel2))
                    {

                        if (InstruçãoLinha[i].Contains("+"))
                        {
                            string antes = VariaveisNome[0];
                            for (int j = 0; j < VariaveisNome.Count(); j++)
                            {
                                if (VariaveisNome[j] != antes)
                                {
                                    AssemblyText += $"ADD {antes}, {VariaveisNome[j]} \n";
                                }
                                antes = VariaveisNome[j];
                            }
                        }
                        else if (InstruçãoLinha[i].Contains("-"))
                        {
                            string antes = VariaveisNome[0];
                            for (int j = 0; j < VariaveisNome.Count(); j++)
                            {
                                if (VariaveisNome[j] != antes)
                                {
                                    AssemblyText += $"SUBT {antes}, {VariaveisNome[j]} \n";
                                }
                                antes = VariaveisNome[j];
                            }
                        }
                        else if (InstruçãoLinha[i].Contains("*"))
                        {
                            string antes = VariaveisNome[0];
                            for (int j = 0; j < VariaveisNome.Count(); j++)
                            {
                                if (VariaveisNome[j] != antes)
                                {
                                    AssemblyText += $"MULT {antes}, {VariaveisNome[j]} \n";
                                }
                                antes = VariaveisNome[j];
                            }
                        }
                        else if (InstruçãoLinha[i].Contains("/"))
                        {
                            string antes = VariaveisNome[0].ToString();
                            for (int j = 0; j < VariaveisNome.Count(); j++)
                            {
                                if (VariaveisNome[j].ToString() != antes)
                                {
                                    AssemblyText += $"DIV {antes}, {VariaveisNome[j]} \n";
                                }
                                antes = VariaveisNome[j].ToString();
                            }
                        }
                    }
                }
                if (InstruçãoLinha[i].Contains("END"))
                {
                    break;
                }
            }
        }


        public void SetOrUpdateVariaveisNome(string nome, int? updateValor = null)
        {
            if (!VariaveisNome.Contains(nome))
            {
                VariaveisNome.Add(nome);
                MemóriaValor.Add(-1);
            }
            else if (updateValor != null)
            {
                int i = VariaveisNome.IndexOf(nome);
                MemóriaValor[i] = updateValor;
            }
        }

        public string AumentarLetra(string texto, int começo = 0, char a = 'a', char z = 'z', char zZ = 'Z')
        {
            char[] letras = texto.ToCharArray();
            int lastIndex = letras.Length - 1;

            for (int i = lastIndex; i >= começo; i--)
            {
                if (letras[i] >= a && letras[i] < z)
                {
                    letras[i] = (char)(letras[i] + 1);
                    return new string(letras);
                }
                else if (letras[i] == zZ)
                {
                    letras[i] = a;
                }
            }

            return new string(letras) + a;
        }

        public string GetÚltimoNomeOperador(string nome = "A")
        {
            while (VariaveisNome.Contains(nome))
            {
                nome = AumentarLetra(nome, 0, 'A', 'Z');
            }
            return nome;
        }

        public string AddVariávelOperador(string nome = "A")
        {
            nome = GetÚltimoNomeOperador(nome);

            VariaveisNome.Add(nome);
            MemóriaValor.Add(0);
            return nome;

        }

        public void BOOL_OP(string op, string a, string b, string nomeA, string nomeB)
        {
            AssemblyText += $"MOVE {nomeA}, {a} \n";
            AssemblyText += $"MOVE {nomeB}, {b} \n";
            AssemblyText += $"{op} {a}, {b} \n";
            AssemblyText += $"MOVE {nomeA}, cr \n";
        }
        public void BOOL(Instrução instrução)
        {
            string cr = Dados.CR;
            // obter booleanos
            List<int> boolÍndices = new();
            Instrução boolOps = new();
            foreach (string op in Dados.OperadoresLogic)
            {
                int i = instrução.IndexOfValor(op);
                if (i != -1 && Dados.OperadoresLogic.Contains(instrução[i].Valor))
                {
                    boolÍndices.Add(i);
                    boolOps.Add(instrução[i]);
                }
            }
            // Sem instruções, retorna
            if (boolÍndices.Count() == 0)
            {
                return;
            }

            // Cria N variáveis
            List<string> vars = new();
            foreach (int i in boolÍndices)
            {
                vars.Add(AddVariávelOperador());
                vars.Add(AddVariávelOperador());
            }

            // Enquanto houver operador, pega com maior precedência, calcula e remove
            while (boolOps.Count() > 0)
            {
                int iMaxPreced = boolOps.GetFirstOfMaxPrecedência();
                int iVarOp = boolÍndices[0];
                string op = instrução[iMaxPreced].Valor;
                string a = instrução[iMaxPreced - 1].Valor;
                string b = instrução[iMaxPreced + 1].Valor;
                string nomeA = VariaveisNome[iVarOp];
                string nomeB = VariaveisNome[iVarOp + 1];
                if (op == "<")
                {
                    BOOL_OP("CMENOR", a, b, nomeA, nomeB);
                    VariaveisNome.Remove(nomeB);
                }
                if (op == ">")
                {
                    AssemblyText += $"CMAIOR {a}, {b} \n";
                }
                if (op == "==")
                {
                    AssemblyText += $"MOVE A, {a} \n";
                    AssemblyText += $"MOVE B, {b} \n";
                    AssemblyText += $"MOVE A, CR \n";
                    AssemblyText += $"CMP A, B \n";
                    AssemblyText += $"MOVE A, CR \n";
                }
                if (op == "!=")
                {
                    AssemblyText += $"CMP {a}, {b} \n";
                    AssemblyText += $"JFALSE {a}, {b} \n";
                }
            }

        }


        public void WHILE(List<Instrução> instruções)
        {
            /*
                WHILE (expressão booleana)
                    <INSTRUÇÕES>
                END

                IF (expressão booleana)
                    <INSTRUÇÕES>
                END
                ---
                MOV fimWhile, 32
                while:
                
                -- codigo

                CMP fimWhile, 1
                JFALSE while
            */
            string nome = "WHILE";
            foreach (Instrução instrução in instruções)
            {
                // procura se WHILE existe na linha
                int índiceWHILE = instrução.IndexOfValor(nome);
                // Se não existir, vai para a próxima isntrução
                if (índiceWHILE == -1)
                {
                    continue;
                }

                // Transforma tudo dentro de WHILE em assembly

            }
        }


        // Rodar e remover o operador usado
        // - Executa getListaOperadores - contém o token e a posição na instrução

        public MapOperadores GetMapOperadores(Instrução instrução)
        {
            // Obtém um map do índice e token de cada operador
            MapOperadores mapOperadores = new();
            for (int i = 0; i < instrução.Count(); i++)
            {
                Token token = instrução[i];
                if (token.Tipos.Contains(TokenTipo.Operador))
                {
                    mapOperadores.Add(i, token);
                }
            }
            return mapOperadores;
        }

        // Somar:
        // - pega a 1a ocorrência do operador,
        // - soma o anterior com o próximo
        // Usa o mesmo nome usado na variável, por enquanto serve
        public void OperaçãoSoma(Instrução instrução, int índiceOperação)
        {
            string a = instrução[índiceOperação - 1].Valor;
            string b = instrução[índiceOperação + 1].Valor;


        }

        public void Operação(Instrução instrução)
        {
            // - Pega uma instrução
            // - obtém uma lista de operadores com o índice da instrução
            MapOperadores mapOperadores = GetMapOperadores(instrução);

            // - verifica precedência
            int índiceMaiorPrecedência = instrução.GetFirstOfMaxPrecedência();
            Token operador = instrução[índiceMaiorPrecedência];

            // - executa cada operação
            if (operador.Valor == "+")
            {
                OperaçãoSoma(instrução, índiceMaiorPrecedência);
            }

        }

    }
}