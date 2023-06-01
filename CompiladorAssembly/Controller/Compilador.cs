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
            GetValoresVariavel();
            return Lista;
        }
        public List<string> Tokens()
        {
            List<string> tokens = new List<string>
            {
                "VAR",
                "WHILE",
                "END",
                "IF",
                "FUNCTION",
                "RETURN",
                "LER",
                "ESCREVER"
            };
            return tokens;
        }
        public void GetValoresVariavel()
        {
            List<string> resultados = new List<string>();
            foreach (string tokem in Tokens())
            {
                Regex regex = new Regex(tokem, RegexOptions.IgnoreCase); // IgnoreCase para buscar independentemente de maiúsculas e minúsculas
                foreach (string texto in Lista)
                {
                    if (regex.IsMatch(texto))
                    {
                        resultados.Add(texto);
                    }
                }
            }
        }
    }
}
