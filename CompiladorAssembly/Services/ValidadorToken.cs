using CompiladorAssembly.Models;
using CompiladorAssembly.Controllers;
using System.Text.RegularExpressions;

namespace CompiladorAssembly;

public static class ValidadorToken
{

    public static void ValidarInstruçãoToken(List<Token> instruçãoToken, CompiladorDados compiladorDados)
    {
        ValidarErroLéxico(instruçãoToken, compiladorDados);
    }


    // Validar erro léxico


    // Verifica se o caractere é válido na liguagem de programação
    // É válido se for separador, operador, letras ou números.
    private static bool ÉCaractereVálido(char caractere, CompiladorDados compiladorDados)
    {
        bool ÉSeparador = compiladorDados.Separadores.Contains(caractere.ToString());
        bool ÉOperador = compiladorDados.Operadores.Contains(caractere.ToString());
        return char.IsLetterOrDigit(caractere) || ÉSeparador || ÉOperador;
    }

    // Apenas verifica se cada caractere é válido na linguagem
    // Se for inválido, lança um erro
    private static void ValidarErroLéxico(List<Token> instruçãoComTokens, CompiladorDados compiladorDados)
    {
        // Para cada token na instrução, pega cada caractere do token e valida.
        foreach (Token token in instruçãoComTokens)
        {
            // Para cada caractere do token, valida.
            foreach (char caractere in token.Valor)
            {
                // Se não for caractere válido, lança um erro.
                if (!ÉCaractereVálido(caractere, compiladorDados))
                {
                    throw new ArgumentException($"Erro léxico: o caractere é inválido: {caractere}");
                }
            }
        }
    }

    // Validar erro sintático

    private static void ValidarErroSintático(List<Token> instruçãoToken, CompiladorDados compiladorDados)
    {
        // Implementar as validações sintáticas aqui
        // Verificar se variáveis existem, se funções existem, se parâmetros estão corretos, etc.
    }

    // 

    private static void ValidarErroSemântico(List<Token> instruçãoToken, CompiladorDados compiladorDados)
    {
        // Implementar as validações semânticas aqui
        // Verificar se variáveis, palavras-chave ou funções existem antes de serem usadas
    }

}
