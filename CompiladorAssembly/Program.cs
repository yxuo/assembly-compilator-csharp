using CompiladorAssembly.Controller;

namespace CompiladorAssembly
{
    class Program
    {
        static void Main(string[] args)
        {
            Compilador compilador= new Compilador();
            compilador.LerArquivo();
        }
    }
}
