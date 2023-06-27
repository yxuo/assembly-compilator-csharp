using System.Collections;

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
        private string valor = "";
        public int Precedência { get; set; } = -1;
        public int Ordem
        {
            get
            {
                if (Precedência == -1)
                {
                    return Precedência;
                }
                return 3 - Precedência;
            }
        }
        public string Valor
        {
            get
            {
                return this.valor;
            }
            set
            {
                // set precedência
                List<string> precedência = new() { "+", "-", "*", "/" };
                Precedência = precedência.IndexOf(value);
                // set valor
                this.valor = value;
            }
        }

        public Token(string valor = "", List<TokenTipo>? tipos = null)
        {
            Tipos = tipos ?? new List<TokenTipo> { };
            this.Valor = valor;
        }

        public override string ToString()
        {
            string tiposString = string.Join(", ", Tipos);
            string retorno = $"Token: '{this.valor}'  Tipos: [{tiposString}]";
            retorno += $" Precedência: '{Precedência}'";
            // }
            return retorno;
        }

    }

    public class Instrução : List<Token>
    {
        public int IndexOfValor(string nome)
        {
            for (int i = 0; i < this.Count(); i++)
            {
                string valor = this[i].Valor;
                if (valor == nome)
                {
                    return i;
                }
            }
            return -1;
        }

        public int GetFirstOfMaxPrecedência()
        {
            if (!this.Any())
            {
                return -1;
            }
            var maxObject = this.MaxBy(x => x.Precedência);
            int maxIndex = this.FindIndex(x => x == maxObject);
            return maxIndex;
        }

        public int IndexOfTipo(TokenTipo tipo)
        {
            for (int i = 0; i < this.Count(); i++)
            {
                if (this[i].Tipos.Contains(tipo))
                {
                    return i;
                }
            }
            return -1;
        }

        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < this.Count(); i++)
            {
                result += $"[{i}] {this[i]}\n";
            }
            return result;
        }

    }
    public class Instrução0
    {
        public List<Token> Tokens { get; set; }
        public Instrução0(List<Token>? tokens = null)
        {
            Tokens = tokens ?? new List<Token>();
        }

        public IEnumerator<Token> GetEnumerator()
        {
            return Tokens.GetEnumerator();
        }

        public int IndexOf(Token item)
        {
            return Tokens.IndexOf(item);
        }

        public int IndexOfValor(string nome)
        {
            for (int i = 0; i < Tokens.Count(); i++)
            {
                string valor = Tokens[i].Valor;
                if (valor == nome)
                {
                    return i;
                }
            }
            return -1;
        }

        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < Tokens.Count(); i++)
            {
                result += $"[{i}] {Tokens[i]}\n";
            }
            return result;
        }

        public Token this[int index]
        {
            get
            {
                return Tokens[index];
            }
            set
            {
                Tokens[index] = value;
            }
        }

        // public Token GetTokenByValor(string valor){
        // foreach ()
        // }

        // public static override int Count()
        // {
        //     return this.Tokens.Count();
        // }

    }

}