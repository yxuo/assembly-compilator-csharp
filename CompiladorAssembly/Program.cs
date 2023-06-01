using CompiladorAssembly.Controller;

namespace CompiladorAssembly
{
    class Program
    {
        static void Main(string[] args)
        {
            Compilador compilador= new Compilador();
            List<string> lista = compilador.LerArquivo();           
        }
    }
}
