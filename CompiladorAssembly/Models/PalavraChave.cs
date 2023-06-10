namespace CompiladorAssembly.Models
{

    public abstract class PalavraChave
    {
        public string Nome { get; set; } = "";

        public abstract string ConverterParaAssembly(string[] tokens);
    }


    class PalavraChaveGenérica : PalavraChave
    {
        public PalavraChaveGenérica(string ?nome)
        {
            if (nome != null)
            {
                Nome = nome;
            }
        }
        public override string ConverterParaAssembly(string[] tokens)
        {
            return "VAR assembly code";
        }
    }
}