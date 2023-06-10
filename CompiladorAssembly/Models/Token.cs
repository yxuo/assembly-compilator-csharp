namespace CompiladorAssembly.Models
{

    public enum TokenTipo
    {
        Indefinido,
        Variável,
        Operador,
        Literal,
        Registrador,
        PalavraChave
    }

    public class Token
    {
        public TokenTipo Tipo { get; set; }
        public string Valor { get; set; }

        public Token(string valor = "", TokenTipo tipo = TokenTipo.Indefinido)
        {
            Tipo = tipo;
            Valor = valor;
        }

        public override string ToString()
        {
            return "Token: '" + Valor + "'  Tipo: '" + Tipo + "'";
        }

    }

}