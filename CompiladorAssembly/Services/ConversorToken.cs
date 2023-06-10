using System;
using System.Collections.Generic;
using CompiladorAssembly.Models;
using CompiladorAssembly.Controllers;

namespace CompiladorAssembly.Services
{
    public static class ConversorToken
    {

        /// <summary>
        /// A partir de uma linha (instrução), gera uma lista de tokens
        /// </summary>
        /// <param name="instrução"> Uma linha de código em pseudocódigo.</param>
        /// <returns>Uma lista de tokens.</returns>
        public static List<Token> ConverterInstrucao(string instrucao, CompiladorDados compiladorDados)
        {
            List<Token> tokens = new();
            string[] palavras = instrucao.Split(' ');

            // Gerar tokens com seus tipos
            for (int i = 0; i < palavras.Length; i++)
            {
                string palavra = palavras[i].Trim();
                Token token = new(palavra);

                if (StrTokenEVariavel(palavras, i, compiladorDados))
                {
                    token.Tipo = TokenTipo.Variável;
                }
                else if (StrTokenERegistrador(palavra, compiladorDados))
                {
                    token.Tipo = TokenTipo.Registrador;
                }
                else if (StrTokenEOperador(palavra, compiladorDados))
                {
                    token.Tipo = TokenTipo.Operador;
                }
                else if (StrTokenELiteral(palavra))
                {
                    token.Tipo = TokenTipo.Literal;
                }
                else if (StrTokenEPalavraChave(palavra, compiladorDados))
                {
                    token.Tipo = TokenTipo.PalavraChave;
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
            return compiladorDados.Operadores.Contains(palavra);
        }

        private static bool StrTokenELiteral(string palavra)
        {
            return int.TryParse(palavra, out _);
        }

        private static bool StrTokenERegistrador(string palavra, CompiladorDados compiladorDados)
        {
            return compiladorDados.Registradores.ContainsKey(palavra);
        }

    }
}
