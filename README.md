# Sistema de Gerenciamento de Filas de Atendimento
Este projeto é uma API desenvolvida em **ASP.NET Core** para gerenciar filas de atendimento, com funcionalidades como registro de clientes e priorização de atendimentos. 

Foi baseado no desafio do Rafael Coelho: 

[Desafio de Programação: Sistema de Fila de Espera](https://racoelho.com.br/listas/desafios/fila-de-espera).

## Funcionalidades Principais

### Autenticação e Autorização:

1. Registro e login de usuários.
2. Geração de tokens JWT para autenticação.
3. Implementação de refresh tokens para renovação de tokens sem necessidade de novo login.
4. Controle de acesso baseado em roles (User e Admin).

## Gerenciamento de Filas:

1. Criação de filas de atendimento.
2. Registro de clientes em filas.
3. Priorização de clientes com base em categorias e níveis de prioridade.
4. Chamada do próximo cliente da fila.

## Tecnologias Utilizadas

### Backend:

1. ASP.NET Core 9.0
2. Entity Framework Core (Code-First)
3. JWT (JSON Web Tokens)
4. BCrypt para hash de senhas

### Banco de Dados:

SQL Server

### Padrões de Projeto:

1. Domain-Driven Design (DDD)
2. Repository Pattern
3. Generic Repository

### Ferramentas:

Swagger/OpenAPI para documentação da API

## Como Executar o Projeto

### Pré-requisitos

1. .NET SDK 9.0
2. SQL Server ou Docker para rodar o SQL Server em um container.

### Passos para Execução

#### Clone o repositório:

```
git clone https://github.com/seu-usuario/sistema-filas-atendimento.git
cd sistema-filas-atendimento
```

#### Configure o banco de dados:

1. Crie um banco de dados no SQL Server.

2. Atualize a string de conexão no arquivo appsettings.json:

```
"ConnectionStrings": {
  "DefaultConnection": "Server=Server=localhost\\sqlexpress;Initial Catalog=QueueDb; Integrated Security=True; TrustServerCertificate=true;"
}
```

3. Aplique as migrações:

```
dotnet ef database update
```

4. Inicie a aplicação:

```
dotnet run
```

A API estará disponível em http://localhost:5282.

## Endpoints Principais

### Autenticação

#### Registro de Usuário:

`POST /api/account/register`

#### Login:

`POST /api/account/login`

#### Refresh Token:

`POST /api/account/refresh-token`

### Filas de Atendimento

#### Listar Clientes Da Fila

`GET api/queue`

#### Chamar Próximo Cliente

`POST api/queue/next`

### Clientes

#### Registar Clientes Na Fila

`POST api/client`

#### Consultar Posição do Cliente

`GET api/client/{id}/position`

#### Cancelar Registro de Cliente

`DELETE api/client/{id}`


## Estrutura do Projeto
```
QueueSystem/
├── QueueSystem.API/
│    ├── Controllers/
│    ├── appsettings.json
│    ├── Program.cs
│    ├── Startup.cs
├── QueueSystem.Application/
│    ├── Dtos/
│    ├── Implements/
│    ├── Services/ 
├── QueueSystem.Domain/
│    ├── Entities/
│    ├── Enums/
├── QueueSystem.Infra/
│    ├── Data/
│    ├── Repositories/
```


