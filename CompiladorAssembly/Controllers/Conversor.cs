namespace CompiladorAssembly;
using System.Text.RegularExpressions;

public class ConversorAssembly
{
    // public List<string> Lista { get; set; } = new List<string>();
    // public List<string> Resultados { get; set; } = new List<string>();
    // public int Cont { get; set; }

    // public List<string> LerArquivo()
    // {
    //     using (StreamReader sr = new("./Teste.txt"))
    //     {
    //         while (!sr.EndOfStream)
    //         {
    //             string line = sr.ReadLine().Trim();
    //             Lista.Add(line);
    //             Cont++;
    //         }
    //     }
    //     GetValoresVar(Lista);
    //     return Lista;
    // }
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
