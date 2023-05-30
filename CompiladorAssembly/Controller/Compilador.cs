using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompiladorAssembly.Controller
{
    class Compilador
    {
        public Dictionary<int,dynamic> Lista { get; set; } 
        public void LerArquivo()
        {
            var line = File.ReadAllLines("./Teste.txt");
            for (int i = 0; i < line.Length; i++)
            {
                foreach (var item in line[i])
                {
                    Lista.Add(i, item);
                }
            }
        }
    }
}
