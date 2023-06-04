using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CompiladorAssembly.Controller
{
    class Compilador
    {
        public List<string> Lista { get; set; } = new List<string>();
        public Dictionary<int,string> Resultados { get; set; } = new Dictionary<int, string>();
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
                    GetValoresVar(line,Cont);
                }
            }
            //Essa função vai separar os tokens var
            return Lista;
        }
        //public List<string> Tokens()
        //{
        //    List<string> tokens = new List<string>
        //    {
        //        "VAR",
        //        "WHILE",
        //        "END",
        //        "IF",
        //        "FUNCTION",
        //        "RETURN",
        //        "LER",
        //        "ESCREVER"
        //    };
        //    return tokens;
        //}
        public Dictionary<int, string> GetValoresVar(string valor,int count)
        {           
            Regex regex = new Regex("var", RegexOptions.IgnoreCase); // IgnoreCase para buscar independentemente de maiúsculas e minúsculas
            if (regex.IsMatch(valor))
            {
                Resultados.Add(count,valor);
            }
            return Resultados;
        }
    }
}
