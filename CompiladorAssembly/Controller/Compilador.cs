using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public void GetValoresVariavel()
        {
            string valorProcurado = "VAR";
            int indice = Lista.FindIndex(linha => linha == valorProcurado);
            string texto = Lista[indice];
        }
    }
}
