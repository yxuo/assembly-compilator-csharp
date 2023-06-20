namespace CompiladorAssembly.Models
{

    public abstract class PalavraChave
    {
        public abstract string Nome { get; set; }

        public abstract List<TokenTipo> Parâmetros { get; set; }

        public abstract string ConverterParaAssembly(string[] tokens);
    }


    class PalavraChaveGenérica : PalavraChave
    {

        public override string Nome { get; set; } = "";

        public override List<TokenTipo> Parâmetros { get; set; } = new List<TokenTipo> {};
        public PalavraChaveGenérica(string? nome, List<TokenTipo> ?parâmetros = null)
        {
            if (nome != null)
            {
                Nome = nome;
            }

            if (parâmetros != null)
            {
                Parâmetros = parâmetros;
            }

        }

        public override string ConverterParaAssembly(string[] tokens)
        {
            return "VAR assembly code";
        }
    }
}