# assembly-compilator-csharp

Compilador assembly em c#

## TODO

- [ ] Desenvolver a estrutura a ser criada

## Desenvolvimento

Veja o [Relatório de Desenvolvimento](./doc/relatorio-desenvolvimento.md)

## Requisitos

1. Todas as variáveis são inteiras.

2. A linguagem permite a definição de variáveis no formato:
   ```
   VAR <nome da variável>
   ```
   Somente uma por linha.

3. Todos os identificadores só poderão usar letras minúsculas ou maiúsculas, sendo a linguagem case sensitive.

4. Os operadores presentes na linguagem são os seguintes: =, +, -, *, /, ==, <, > e !

5. Os comandos da linguagens são os seguintes:
   ```
   WHILE (expressão booleana)
       <INSTRUÇÕES>
   END

   IF (expressão booleana)
       <INSTRUÇÕES>
   END
   ```

6. A linguagem permite a definição de funções que retornam ao final valores inteiros. Elas devem ser declaradas antes do seu uso e não será necessário implementar uma stack para o seu funcionamento (i.e., as variáveis terão posições estáticas).
   ```
   FUNCTION <nome> : <param1>, <param2> ...
       <INSTRUÇÕES>
       RETURN <variável> ou <literal>
   END
   ```

7. Para a leitura e escrita, a linguagem tem as funções:
   ```
   <variável> = LER 
   ESCREVER <variável>
   ```

Exemplo de Código
```
FUNCTION SOMAR : a, b
     VAR resultado
     resultado = a + b
     RETURN resultado
END

VAR x
VAR y
x = LER
y = LER
x = SOMAR(x,y)
ESCREVER x
```
