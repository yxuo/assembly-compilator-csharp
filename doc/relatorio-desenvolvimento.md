# Relatório de desenvolvimento do projeto

1. Requisitos (em paralelo)
2. Entender o projeto
3. Sumarizar o que deve ser feito
4. Analisar como fazer de forma rápida mas atingindo o objetivo proposto
5. Requisitos

# 1. Requisitos

- Programa que lê linguagem (pseudocódigo) e converte para executável (assembly do trabalho anterior).
- 

# 2. Estrutura

- CompiladorAssembly:classe
  - tokens:lista(str)
  - Lista de variáveis

# 2. Entender o código (professor)

1. Pega o código e separa em array de str para cada linha (instrução)
2. Para cada array de insturção, separa em tokens.
3. Para cada linha, classifica o token
4. Verifica 

### Erros de código

- Se tiver 2 variáveis sem operador, é erro léxico

### Exemplo
Precedência = `/ * - +`
```
x = y + z * k - z
```

1. Operador de maior precedência: `*`, Resolver `z * K` (à direita)

   assembly:
   ```
   MOVE A,Z
   MOVE B,K
   MULT A,B
   ```


## O que o projeto faz, em resumo

Primeira coisa: O que um compilador faz?
> Compilador → Apenas cria um executável a partir do código fonte
> 
> Interpretador → Lê e executa código fonte linha por linha.

Com isso sabemos que projeto é um simples conversor.

## Passos

Temos um impasse que é saber quais os passos a fazer.

- Ler cada linha e converter para token
- Detectar tipos de cada token

## 5. Requisitos

1. Estruturar código
2. Gerar Tokens
3. Validar Tokens
4. Gerar Assembly
5. Testes

### Validar tokens

Como validar os tokens?
* Erro léxico
  * Caractere inválido

* Erro sintático:
  * Padrão inválido
    * `VAR <nome válido não usado>`
    * `<palavra-chave> + N <parâmetros>`
    * `N <nome válido / variável / literal> + N +1 <operador>`
      Exemplo: 3 literais, 2 operadores
  * variável não existe
  * Função não existe
  * Parâmetros incorretos
  * Número de parâmetros incorretos

* Erro semântico
  * Variável, palavra/chave ou função não existe ainda.