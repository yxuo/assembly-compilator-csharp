using CompiladorAssembly.Models;
using CompiladorAssembly.Controllers;
using System.Text.RegularExpressions;

namespace CompiladorAssembly.Services
{
    public static class ConversorToken
    {

        /// <summary>
        /// A partir de uma linha (instrução), gera uma lista de tokens
        /// </summary>
        /// <param name="instrução"> Uma linha de código em pseudocódigo.</param>
        /// <returns>Uma lista de tokens.</returns>
        public static Instrução ConverterInstrução(string instrucao, CompiladorDados compiladorDados)
        {
            Instrução tokens = new();
            string[] palavras = Utils.SplitWithDelimiter(instrucao, compiladorDados.Separadores).ToArray();
            // foreach (string s in palavras) { Console.Write("[" + s + "] "); } Console.WriteLine();

            // Gerar tokens com seus tipos
            // List<TokenTipo> parâmetros = new() { };
            for (int i = 0; i < palavras.Length; i++)
            {
                string palavra = palavras[i].Trim();
                if (palavra.Length == 0)
                {
                    continue;
                }
                Token token = new(palavra);


                // Adiciona os tipos
                if (StrTokenEVariavel(palavras, i, compiladorDados))
                {
                    token.Tipos.Add(TokenTipo.Variável);
                }

                if (StrTokenERegistrador(palavra, compiladorDados))
                {
                    token.Tipos.Add(TokenTipo.Registrador);
                }

                if (StrTokenEOperador(palavra, compiladorDados))
                {
                    token.Tipos.Add(TokenTipo.Operador);
                }

                if (StrTokenELiteral(palavra))
                {
                    token.Tipos.Add(TokenTipo.Literal);
                }

                if (StrTokenENomeValido(palavra))
                {
                    token.Tipos.Add(TokenTipo.NomeVálido);
                }

                // Se o token não possui tipo, adiciona Indefinido para indicar que foi analisado.
                if (token.Tipos.Count == 0)
                {
                    token.Tipos.Add(TokenTipo.Indefinido);
                }

                // Se for palavra-chave, o tipo de token será apenas este.
                if (StrTokenEPalavraChave(palavra, compiladorDados) && palavra != null)
                {
                    token.Tipos.Clear();
                    token.Tipos.Add(TokenTipo.PalavraChave);
                }

                tokens.Add(token);
            }

            return tokens;
        }


        private static bool StrTokenEPalavraChave(string palavra, CompiladorDados compiladorDados)
        {
            return compiladorDados.GetPalavrasChaveNome().Contains(palavra);
        }

        private static bool StrTokenEVariavel(
        string[] palavras,
        int índicePalavra,
        CompiladorDados compiladorDados
        )
        {
            if (índicePalavra > 0)
            {
                string palavraAnterior = palavras[índicePalavra - 1];
                return StrTokenEPalavraChave(palavraAnterior, compiladorDados);
            }

            return false;
        }

        private static bool StrTokenEOperador(string palavra, CompiladorDados compiladorDados)
        {
            return compiladorDados.OperadoresMat.Contains(palavra);
        }

        /// <summary>
        /// Se o token retorna Literal (apenas números)
        /// </summary>
        private static bool StrTokenELiteral(string palavra)
        {
            return int.TryParse(palavra, out _);
        }

        private static bool StrTokenERegistrador(string palavra, CompiladorDados compiladorDados)
        {
            return compiladorDados.Registradores.ContainsKey(palavra);
        }


        /// <summary>
        /// Se o token retorna nome válido de variável e afins. <para />
        /// Como saber se o token é nome válido de variável?
        /// - O primeiro caractere não pode ser número <br />
        /// - Não pode conter caracteres especiais <br />
        /// </summary>
        private static bool StrTokenENomeValido(string palavra)
        {
            return Regex.IsMatch(palavra, @"^(?![0-9])(?!.*[\W]).*$");
        }

    }
}
