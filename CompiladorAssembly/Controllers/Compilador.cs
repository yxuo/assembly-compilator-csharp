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
        public IReadOnlyList<string> Operadores { get; } = new List<string> { "=", "+", "-", "*", "/", "==", "<", ">", "!" };
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

    }

    class Compilador
    {
        // Dados usados vários lugares desse programa
        public CompiladorDados Dados { get; set; } = new();

        // Dados usados apenas nessa classe
        public List<Instrução> ListaInstruções { get; set; } = new List<Instrução>();

        public string? Variavel { get; set; }
        public string Variavel2 { get; set; } = "";
        public string Teste { get; set; } = "";
        public bool IniciarLeitura { get; set; } = false;
        public List<string>? VariaveisNome { get; set; } = new();
        public List<string> InstruçãoLinha { get; set; } = new List<string>();
        public List<dynamic>? VariaveisValor { get; set; } = new();
        public List<string> ÍndiceVariáveisLocais { get; set; } = new List<string>();

        public Dictionary<char, int> Variáveis { get; set; } = new();
        public string? AssemblyText { get; set; }

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

        public void GerarTokens(StreamReader arquivo)
        {
            // esvaziar ListaInstruções
            ListaInstruções.Clear();

            // Para cada linha no arquivo, transforma em token
            // <Enquanto a leitura não estiver no final do arquivo>            
            while (!arquivo.EndOfStream)
            {
                string? linha = arquivo.ReadLine();

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
                InstruçãoLinha.Add(linha);
                // 1 linha = 1 instrução
                // Converter instrução em string para instrrução em tokens
                Instrução instrução = ConversorToken.ConverterInstrução(linha, Dados);

                // Debug
                // Console.WriteLine(linha);
                // Console.WriteLine(instrução + "\n");
                // Console.WriteLine("---------");


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
            // foreach (Instrução instrução in listaInstrução)
            // {
            //     Operador(instrução);
            // }
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
                AssemblyText += $"MOVE {VariaveisNome[j]}, {VariaveisValor[j]} \n";
            }
        }
        public void INT_1()
        {
            Console.WriteLine("Digite o valor: ");
            Variavel = Console.ReadLine();
            VariaveisValor.Add(Variavel);
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

        public void WHILE(List<Instrução> instruções)
        {
            string nome = "WHILE";
            int índiceNome = VariaveisNome.IndexOf(nome);
            // if (índiceNome )
        }


        public void VAR(List<Instrução> instruções)
        {
            foreach (Instrução instrução in instruções)
            {
                string nome = "VAR";
                int índiceVAR = instrução.IndexOfValor(nome);
                if (índiceVAR == -1)
                {
                    continue;
                }

                string x = instrução[índiceVAR + 1].Valor;

                SetVariável(x, 0);
                // AssemblyText += $"MOVE {VariaveisNome[j]},{VariaveisValor[j]} \n";

            }

            foreach (Instrução instrução in instruções)
            {
                VAR_FUNCTION(instrução);
            }
        }

        public void VAR_FUNCTION(Instrução instrução)
        {
            // variáveis
            string nome = "FUNCTION";
            int índiceVAR = instrução.IndexOfValor(nome);
            if (índiceVAR == -1)
            {
                return;
            }

            // pega item 3+ e adiciona nomes váliddos

            for (int i = índiceVAR + 3; i < instrução.Count(); i++)
            {
                Token token = instrução[i];
                if (token.Tipos.Contains(TokenTipo.NomeVálido))
                {
                    string x = token.Valor;
                    SetVariável(x, 0, nome);
                    // AssemblyText += $"MOVE {VariaveisNome[j]},{VariaveisValor[j]} \n";
                }
            }
        }



        public void Operador(Instrução instrução)
        {
            // operadores
            Dictionary<char, int> mapOpPrecedência = new()
        {
            { '/', 4 },
            { '*', 3 },
            { '-', 2 },
            { '+', 1 },
        };
            char[] op = mapOpPrecedência.Keys.ToArray();
            int[] prec = mapOpPrecedência.Values.ToArray();

            // 1. Montar posição de cada operador
            Dictionary<int, char> opÍndice = new();
            for (int i = 0; i < instrução.Count(); i++)
            {
                // Transforma token em valor
                Token token = instrução[i];
                string valor = token.Valor;
                if (valor == null)
                {
                    continue;
                }
                // Adiciona operador e índice se existir
                bool valorÉOperador = valor.Any(c => op.Contains(c));
                if (valorÉOperador)
                {
                    opÍndice.Add(i, valor[0]);
                }
            }

            if (opÍndice.Count() == 0)
            {
                return;
            }


            // Transformar token de volta em string para calcular
            string expressão = "";
            int i0 = opÍndice.Keys.ToList()[0] - 1;
            // Pega o item antes do 1o token e transforma em string
            foreach (Token token1 in instrução.GetRange(i0, instrução.Count() - i0))
            {
                // obtém valor da variável
                string var1 = token1.Valor;
                if (token1.Tipos.Contains(TokenTipo.Operador))
                {
                    expressão += var1;
                }
                else
                {
                    dynamic índice = VariaveisNome.IndexOf(var1);
                    // System.Console.WriteLine($"VN len {VariaveisNome.Count()}");
                    // for (int j = 0; j < VariaveisNome.Count; j++)
                    // {
                    //     System.Console.Write($"VN[{j}: {VariaveisNome[j]}  ");
                    //     System.Console.WriteLine($"VV[{j}]: {GetVariávelValor(VariaveisNome[j])}");
                    // }
                    // System.Console.WriteLine($"Valor[{índice}] = {var1}");
                    // int varValor = vv;
                    // expressão += varValor.ToString();
                }
            };
            System.Console.WriteLine(expressão);
            // DataTable dt = new();
            // var v = dt.Compute("1+2*3+4", "");
            // System.Console.WriteLine(expressão);

            // Debug operação
            // foreach (KeyValuePair<char, int> item in opÍndice)
            // {
            //     System.Console.WriteLine($"{item.Key}:{item.Value}");
            // }

            // 2. 

            // 2. Para cada operador, verifica se dá para juntar seguindo a precedência

            // // while (opÍndice.Count() > 0)
            // // {
            // // obter índice do 1o maior vlaor (precedência)
            // // obtém chaves
            // List<char> chaves = opÍndice.Values.ToList();
            // // transforma em precedência
            // List<int> precedências = chaves.Select(c => mapOpPrecedência[c]).ToList();
            // // pega o primeiro maior

            // int MaiorPrecedênciaÍndice = precedências.IndexOf(precedências.Max());
            // System.Console.WriteLine(chaves);
            // System.Console.WriteLine($"MP {MaiorPrecedênciaÍndice}");
            // // converte
            // foreach (KeyValuePair<int, char> item in opÍndice)
            // {
            //     System.Console.WriteLine($"{item.Key}:{item.Value}");

            //     // verificar se antes e depois tem variável
            //     bool antesExiste = item.Key > 0;
            //     bool depoisExiste = item.Key < instrução.Tokens.Count();
            //     if (antesExiste && depoisExiste)
            //     {
            //         // bool 
            //     }
            // }
        }

        public char GetRelativeLetter(int offset)
        {
            return (char)('a' + offset);
        }



        public void SetÍndiceVariáveisLocais(string função)
        {
            int índice = ÍndiceVariáveisLocais.IndexOf(função);
            if (índice == -1)
            {
                ÍndiceVariáveisLocais.Add(função);
            }
        }

        public string GetNomeVariável(string nome, string função)
        {
            string novoNome = nome;
            int índice = ÍndiceVariáveisLocais.IndexOf(função);
            if (índice != -1)
            {
                novoNome += GetRelativeLetter(índice);
            }
            return novoNome;
        }

        public void SetVariável(string nome, dynamic valor, string função = "")
        {
            // Adiciona prefixo para variável local por função
            if (função.Length > 0)
            {
                SetÍndiceVariáveisLocais(função);
            }
            // nome = GetNomeVariável(nome, função);
            Console.WriteLine($"[VAR] {nome} = {valor}");

            // ser variável
            int índiceNome = VariaveisNome.IndexOf(nome);
            if (índiceNome == -1)
            {
                VariaveisNome.Add(nome);
                VariaveisValor.Add(valor);
            }
            else
            {
                VariaveisValor[índiceNome] = valor;
            }
        }

        public dynamic GetVariávelValor(string nome, string função = "")
        {
            // Se for variável local, adiciona sufixo
            if (função.Length > 0)
            {
                nome += ÍndiceVariáveisLocais.IndexOf(função);
            }

            int índiceNome = VariaveisNome.IndexOf(nome);
            return VariaveisValor[índiceNome];
        }

        public void DelVariável(string nome, dynamic valor, string função = "")
        {
            nome = GetNomeVariável(nome, função);
            int índiceNome = VariaveisNome.IndexOf(nome);
            VariaveisNome.Remove(nome);
            VariaveisValor.RemoveAt(índiceNome);
        }

        // x = y + x * z - w


        //     int i = item.IndexOfValor(nome);
        //     string x = item.Tokens[i + 1].Valor;
        //     AssemblyText += $"MOVE {x},0 \n";
    }

}

