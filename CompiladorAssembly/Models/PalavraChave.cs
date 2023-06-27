using System;
namespace CompiladorAssembly.Models
{

    public abstract  class PalavraChave
    {
        public abstract string Nome { get; set; }

        public abstract List<TokenTipo> Parâmetros { get; set; }

        public abstract string ConverterParaAssembly(Instrução tokens);
    }


    public class PalavraChaveGenérica : PalavraChave
    {

        public override string Nome { get; set; } = "";

        public override List<TokenTipo> Parâmetros { get; set; } = new List<TokenTipo> { };
        public PalavraChaveGenérica(string? nome, List<TokenTipo>? parâmetros = null)
        {
            if (nome != null)
            {
                Nome = nome;
            }

            if (parâmetros != null)
            {
                Parâmetros = parâmetros;
            }

        }

        public override string ConverterParaAssembly(Instrução tokens)
        {
            return "VAR assembly code";
        }
    }
    // class LER : PalavraChave
    // {

    //     public override string Nome { get; set; } = "LER";

    //     public override List<TokenTipo> Parâmetros { get; set; } = new List<TokenTipo> {
    //         TokenTipo.NomeVálido
    //     };
    //     public LER(){
    //     }

    //     public override string ConverterParaAssembly(Instrução tokens)
    //     {
    //         // Obter parâmetros
    //         bool PalavraChaveEncontrada = false;
    //         foreach (Token token in tokens)
    //         {
    //             if (token.Valor == Nome)
    //             {
    //                 PalavraChaveEncontrada = true;
    //             }
    //         }
    //         if (!PalavraChaveEncontrada)
    //         {
    //             return "";
    //         }

    //         Console.WriteLine("Digite o valor: ");
    //         string valor = Console.ReadLine();
    //         //return $"INT 1,{valor do teclado}";
    //         return $"INT 1,{valor}";
    //     }
    // }
    // public string LER(){

    // }
    // class MOVE : PalavraChave
    // {

    //     public override string Nome { get; set; } = "MOVE";

    //     public override List<TokenTipo> Parâmetros { get; set; } = new List<TokenTipo> {
    //         TokenTipo.NomeVálido, TokenTipo.Variável
    //     };
    //     public PalavraChaveGenérica(string? nome, Instrução? parâmetros = null)
    //     {
    //         if (nome != null)
    //         {
    //             Nome = nome;
    //         }

    //         if (parâmetros != null)
    //         {
    //             Parâmetros = parâmetros;
    //         }

    //     }

    //     public override string ConverterParaAssembly(Instrução tokens)
    //     {
    //         //return $"MOVE {nome_variavel},{valor teclado}";
    //         return "MOVE assembly code";
    //     }
    // }
}