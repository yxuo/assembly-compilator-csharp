using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CompiladorAssembly.Controller
{
    class Compilador
    {
        public List<string> Lista { get; set; } = new List<string>();
        public List<string> Resultados { get; set; } = new List<string>();
        public int Cont { get; set; }
        public List<string> LerArquivo()
        {
            using (StreamReader sr = new StreamReader("./Teste.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine().Trim();
                    Lista.Add(line);
                    Cont++;
                }
            }
            GetValoresVar(Lista);
            return Lista;
        }
        public List<string> GetValoresVar(List<string> valor)
        {
            Regex regex = new Regex("var", RegexOptions.IgnoreCase);// IgnoreCase para buscar independentemente de maiúsculas e minúsculas
            string[] val;
            foreach (var item in valor)
            {
                if (regex.IsMatch(item))
                {
                    val = item.Split(" ");
                    Resultados.Add(val[1]);
                    AtribuiçoesVar(val[1], valor);
                }
            }
            return Resultados;
        }
        public void AtribuiçoesVar(string achar_valores, List<string> valor)
        {
            Regex regex = new Regex(achar_valores);
            string[] separador;
            foreach (var item in valor)
            {
                if (regex.IsMatch(item))
                {
                    if (item.Contains('='))
                    {
                        separador = item.Split("=");
                    }
                }
            }            
        }
    }
}
