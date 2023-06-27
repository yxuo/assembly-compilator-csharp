
string AumentarLetra(string texto, int começo = 0, char a = 'a', char z = 'z', char zZ = 'Z')
{
    char[] letras = texto.ToCharArray();
    int lastIndex = letras.Length - 1;

    for (int i = lastIndex; i >= começo; i--)
    {
        if (letras[i] >= a && letras[i] < z)
        {
            letras[i] = (char)(letras[i] + 1);
            return new string(letras);
        }
        else if (letras[i] == zZ)
        {
            letras[i] = a;
        }
    }

    return new string(letras) + a;
}


static void Main(string[] args)
{
    Console.WriteLine(AumentarLetra("a"));
}
