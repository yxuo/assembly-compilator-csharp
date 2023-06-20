namespace CompiladorAssembly.Models
{

    public enum TokenTipo
    {
        // Padrão
        Indefinido,

        // Usado na linguagem
        Variável,       // nomes
        Operador,       // +-*/
        Literal,        // Números

        // Tipos auxiliares, para ajudar a identificar
        // ! Registrador,    // Talvez não precisemos disso
        PalavraChave,   // e.g. VAR, FUNCTION
        NomeVálido,     // Nome válido de variável ou função
        Registrador,
    }

    public class Token
    {
        public List<TokenTipo> Tipos { get; set; }
        public string Valor { get; set; }

        public Token(string valor = "", List<TokenTipo>? tipos = null)
        {
            Tipos = tipos ?? new List<TokenTipo> {};
            Valor = valor;
        }

        public override string ToString()
        {
            string tiposString = string.Join(", ", Tipos);
            return $"Token: '{Valor}'  Tipos: [{tiposString}]";
        }

    }

}