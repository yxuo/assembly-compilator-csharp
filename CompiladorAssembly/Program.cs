using CompiladorAssembly.Controllers;

namespace CompiladorAssembly
{
    class Program
    {
        static void Main(string[] args)
        {

            Compilador compilador= new();
            compilador.CompilarArquivo();
        }
    }
}
