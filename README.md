# Controle de Gastos Residenciais

Sistema para controle de gastos de uma residência: cadastro de pessoas, cadastro de transações (receitas/despesas) e consulta de totais por pessoa e geral.

## Requisitos

- Node.js `^20.19.0` ou `>=22.12.0`
- .NET SDK `10.0`

## Tecnologias

- **Back-end:** .NET 10 / C#, Entity Framework Core, SQLite
- **Front-end:** React + TypeScript, Axios, Tailwind CSS

## Regras de negócio

- Pessoa possui identificador único (gerado automaticamente), nome e idade.
- Ao deletar uma pessoa, todas as suas transações são apagadas em cascata (configurado no EF Core).
- Transação possui identificador único, descrição, valor, tipo (Despesa/Receita) e a pessoa vinculada.
- Pessoas menores de 18 anos só podem ter transações do tipo **Despesa**.
- A consulta de totais lista, por pessoa, o total de receitas, despesas e saldo (receitas − despesas), além do total geral consolidado de todas as pessoas.

## Como rodar

### Back-end

```bash
cd backend/ControleDeGastos/ControleDeGastos
dotnet run
```

A API sobe em `http://localhost:5032`. As migrations do EF Core são aplicadas automaticamente na inicialização e os dados persistem em um arquivo SQLite (`controle_de_gastos.db`), criado na pasta do projeto.

Documentação interativa da API (Scalar) disponível em `http://localhost:5032/scalar` em ambiente de desenvolvimento.

### Front-end

```bash
cd frontend
cp .env.example .env   # ajuste VITE_API_URL se necessário
npm install
npm run dev
```

A aplicação sobe em `http://localhost:5173` (padrão do Vite).

### Testes (back-end)

```bash
cd backend/ControleDeGastos
dotnet test
```

Cobrem as principais regras de negócio e validações:

- **Pessoas:** cadastro com sucesso, marcação correta de menor de idade, remoção em cascata das transações ao deletar pessoa, erro ao deletar pessoa inexistente.
- **Transações:** bloqueio de receita para menor de idade, permissão de despesa para menor de idade, permissão de receita para maior de idade, erro ao vincular pessoa inexistente, listagem retornando o nome da pessoa.
- **Relatórios:** cálculo de receitas/despesas/saldo por pessoa, pessoa sem transações com totais zerados, saldo negativo quando despesas superam receitas, soma correta do total geral.
- **Validação de tipo (atributo customizado):** aceitação de valores válidos (incluindo variações de maiúsculas/minúsculas), rejeição de valores inválidos com mensagem listando as opções aceitas, e aceitação de valor vazio/nulo (delegando a obrigatoriedade ao `[Required]`).

## Estrutura

```
backend/ControleDeGastos/ControleDeGastos
├── Controllers/       Endpoints da API
├── Services/          Regras de negócio
├── Models/            Entidades de domínio
├── DTOs/              Contratos de entrada/saída
├── Data/              DbContext e configurações do EF Core
├── Exceptions/        Exceções de domínio
├── Middlewares/       Tratamento global de erros

frontend/src
├── api/               Chamadas HTTP (por domínio)
├── components/        Componentes de UI (por domínio)
├── hooks/             Estado e orquestração de dados
├── types/             Contratos TypeScript
```

## Decisões técnicas

- **SQLite** como banco de dados para simplificar a execução local, sem dependências externas, garantindo persistência entre execuções.
- **Exceções de domínio** (`NotFoundException`, `BusinessRuleException`) tratadas centralmente em um `GlobalExceptionHandler`, mantendo os services livres de tratamento de erro repetitivo.
- **Cascade delete** configurado entre `Pessoa` e `Transacao`, garantindo a integridade exigida pela regra de negócio.
- **Atributo de validação customizado** (`ValidEnumStringAttribute`) para validar o campo `Tipo` da transação já na camada de DTO.
