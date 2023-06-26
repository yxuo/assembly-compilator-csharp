using CompiladorAssembly.Models;
using CompiladorAssembly.Controllers;
using System.Text.RegularExpressions;

namespace CompiladorAssembly;

public static class ValidadorToken
{

    public static void ValidarInstruçãoToken(List<Token> instruçãoToken, CompiladorDados compiladorDados)
    {

    }

    private static void ValidarErroLéxico(List<Token> instruçãoToken)
    {
        foreach (Token token in instruçãoToken)
        {
            foreach (char caractere in token.Valor)
            {
                if (!ÉCaractereVálido(caractere))
                {
                    // Erro léxico - caractere inválido
                    Console.WriteLine($"Erro léxico: Caractere inválido '{caractere}' na instrução '{token.Valor}'");
                }
            }
        }
    }

    /// <summary>
    /// Verifica se o caractere é válido de acordo com as regras da linguagem.
    /// </summary>
    /// <param name="caractere">O caractere a ser verificado.</param>
    /// <returns>True se o caractere for válido, False caso contrário.</returns>
    private static bool ÉCaractereVálido(char caractere, CompiladorDados compiladorDados)
    {
        return char.IsLetterOrDigit(caractere) ||
            compiladorDados.Separadores.Contains(caractere.ToString());
    }


    private static void ValidarErroSintático(List<Token> instruçãoToken, CompiladorDados compiladorDados)
    {
        // Implementar as validações sintáticas aqui
        // Verificar se variáveis existem, se funções existem, se parâmetros estão corretos, etc.
    }

    private static void ValidarErroSemântico(List<Token> instruçãoToken, CompiladorDados compiladorDados)
    {
        // Implementar as validações semânticas aqui
        // Verificar se variáveis, palavras-chave ou funções existem antes de serem usadas
    }

}
