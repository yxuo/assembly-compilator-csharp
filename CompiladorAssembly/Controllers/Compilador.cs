using System;
using System.Text.RegularExpressions;
using CompiladorAssembly.Models;
using CompiladorAssembly.Services;

namespace CompiladorAssembly.Controllers
{

    public class CompiladorDados
    {
        public Dictionary<string, string> Registradores { get; set; } = new();
        public IReadOnlyList<string> Operadores { get; } = new List<string> { "=", "+", "-", "*", "/", "==", "<", ">", "!" };
        public List<PalavraChave> PalavrasChave { get; set; } = new List<PalavraChave>
        {
            new PalavraChaveGenérica("FUNCTION"),
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
        public List<List<Token>> Tokens { get; set; } = new();
        public CompiladorDados Dados { get; set; } = new();


        /// <summary>
        /// A partir de um arquivo, executa a compilação.
        /// </summary>
        public void CompilarArquivo()
        {
            using StreamReader sr = new("./Teste.txt");
            while (!sr.EndOfStream)
            {
                string linha = sr.ReadLine().Trim();
                List<Token> tokens = ConversorToken.ConverterInstrucao(linha, Dados);

                Console.WriteLine(linha);

                foreach (Token token in tokens)
                {
                    Console.WriteLine(token.ToString());
                }

                break;
            }
            // GetValoresVar(Lista);
        }

        // public List<string> GetValoresVar(List<string> valor)
        // {
        //     // IgnoreCase para buscar independentemente de maiúsculas e minúsculas
        //     Regex regex = new Regex("var", RegexOptions.IgnoreCase);
        //     string[] val;
        //     foreach (var item in valor)
        //     {
        //         if (regex.IsMatch(item))
        //         {
        //             val = item.Split(" ");
        //             Resultados.Add(val[1]);
        //             AtribuiçoesVar(val[1], valor);
        //         }
        //     }
        //     return Resultados;
        // }

        // public void AtribuiçoesVar(string achar_valores, List<string> valor)
        // {
        //     Regex regex = new Regex(achar_valores);
        //     string[] separador;
        //     foreach (var item in valor)
        //     {
        //         if (regex.IsMatch(item))
        //         {
        //             if (item.Contains('='))
        //             {
        //                 separador = item.Split("=");
        //             }
        //         }
        //     }
        // }

    }
}
