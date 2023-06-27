namespace CompiladorAssembly;

using System;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using CompiladorAssembly.Controllers;
using CompiladorAssembly.Models;

public static class ConversorAssembly
{
    /*
    Funcionalidades
    - Palavras-chave: VAR, LER, ESCREVER, END etc
    - Operador: Cada operador pega 2 itens e junta

    O que o professor falou:
    - Para cada token, você pergunta: É variável? não, É atributo? não, etc.
    */
    public static string? Variavel { get; set; }
    public static string? AssemblyText { get; set; }

    public static string ConverterParaAssembly(Instrução instrução)
    {
        LER(instrução);
        MOVE(instrução);
        if (AssemblyText == null)
        {
            return "";
        }
        else
        {
            return AssemblyText;
        }
    }

    public static void LER(Instrução instrução)
    {
        string nome = "LER";
        // debug
        for (int i = 0; i < instrução.Count(); i++)
        {
            string valor = instrução[i].Valor;
            if (valor == nome)
            {
                Console.WriteLine("Digite o valor: ");
                Variavel = Console.ReadLine();
                AssemblyText += $"INT 1,{Variavel} \n";
            }
        }
    }


    public static void MOVE(Instrução instrução)
    {
        string nome = "VAR";
        for (int i = 0; i < instrução.Count(); i++)
        {
            string valor = instrução[i].Valor;
            if (valor == nome)
            {
                string valor_token = instrução[i + 1].Valor;
                AssemblyText += $"MOVE {valor_token},{Variavel} \n";
            }
        }
    }

}
