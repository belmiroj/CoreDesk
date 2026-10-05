# 🎧 CoreDesk API

Bem-vindo ao repositório da **CoreDesk API**! 🚀

Este projeto é uma Web API desenvolvida em **.NET 10** voltada para o gerenciamento de chamados de suporte (*Helpdesk / Ticket System*). O objetivo principal foi aplicar na prática conceitos de arquitetura em camadas, manipulação de dados com Entity Framework Core e SQL Server, tratamento global de exceções, boas práticas de código e autenticação segura via JWT.

---

## 🛠️ Tecnologias Utilizadas

- **Linguagem & Framework:** C# / .NET 10 (Web API)
- **Banco de Dados:** SQL Server (Express / LocalDB)
- **ORM:** Entity Framework Core 10 (Code First & Migrations)
- **Autenticação e Segurança:** ASP.NET Core Identity & JWT (JSON Web Token)
- **Documentação Interativa:** Swashbuckle / Swagger
- **Versionamento:** Git & GitHub

---

## 🏛️ Arquitetura do Projeto

O projeto foi estruturado seguindo o padrão de **Arquitetura em Camadas (Layered Architecture)** para separação clara de responsabilidades:

```text
src/CoreDesk.API/
├── Controllers/       # Endpoints da API (HTTP Requests & Responses)
├── Services/          # Regras de negócio e validações
├── Repositories/      # Acesso a dados e consultas com EF Core
├── Models/            # Entidades de domínio (Category, Ticket, TicketInteraction)
├── DTOs/              # Data Transfer Objects para input/output de dados
├── Data/              # ApplicationDbContext e Histórico de Migrations
└── Middlewares/       # Middleware de tratamento global de exceções
```

---

## 💼 Funcionalidades Implementadas

### 🎯 Funcionalidades Principais (CRUD & Regras de Negócio)

#### 📁 Categorias (`/api/categorias`)
* Cadastro, listagem, consulta por ID, atualização e remoção de categorias.
* **Regra de Negócio:** Impede a exclusão de categorias que possuem chamados vinculados.

#### 🎫 Chamados (`/api/chamados`)
* **Abertura de Chamado:** Cadastro de solicitações vinculadas a uma categoria existente *(Status inicial: `Open`)*.
* **Atendimento (`PATCH /iniciar`):** Transição de status para `InProgress`.
* **Interações (`POST /interacoes`):** Adição de comentários e notas técnicas de acompanhamento.
* **Encerramento (`PATCH /encerrar`):** Registro da solução e transição de status para `Closed`.
* **Regras de Negócio:**
  * Impede adição de interações em chamados já encerrados.
  * Listagem com suporte a filtros dinâmicos *(por Status e Prioridade)*.

---

### 🛡️ Segurança & Tratamento de Erros

* **Middleware Global de Exceções:** Captura e padroniza erros da aplicação retornando payloads JSON amigáveis.
* **Autenticação JWT:** Endpoints de registro (`/api/auth/register`) e login (`/api/auth/login`) utilizando ASP.NET Core Identity e tokens JWT.

---

## 🚀 Como Executar o Projeto Localmente

### Pré-requisitos
* **SDK do .NET 10** instalado.
* **SQL Server** (Express ou LocalDB) em execução.
* Ferramenta de linha de comando do EF Core (`dotnet-ef`) instalada globalmente:
  ```bash
  dotnet tool install --global dotnet-ef
  ```

### Passo a Passo de Execução

1. **Clonar o repositório:**
   ```bash
   git clone https://github.com/belmiroj/CoreDesk.git
   cd CoreDesk
   ```

2. **Configurar a Connection String:**
   Verifique no arquivo `src/CoreDesk.API/appsettings.json` se a string de conexão é compatível com a sua instância do SQL Server:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=CoreDeskDb;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```

3. **Criar / Atualizar o Banco de Dados:**
   Execute o comando de migração para criar o banco de dados `CoreDeskDb` e todas as tabelas (Identity, Categorias e Chamados):
   ```bash
   dotnet ef database update --project src/CoreDesk.API
   ```

4. **Executar a API:**
   Inicie a aplicação .NET Web API:
   ```bash
   dotnet run --project src/CoreDesk.API
   ```

5. **Acessar a Documentação no Swagger:**
   Abra o navegador no endereço indicado no console após a inicialização (por padrão: `http://localhost:5221/swagger` ou `https://localhost:7000/swagger`).

---

## 🔒 Testando a Autenticação no Swagger

1. Acesse o endpoint `POST /api/auth/register` e crie uma nova conta informando e-mail e senha.
2. Acesse o endpoint `POST /api/auth/login` para autenticar e copiar o token retornado no corpo da resposta.
3. No canto superior direito da página do Swagger, clique no botão **Authorize**.
4. Insira `Bearer <seu_token_aqui>` e clique em **Authorize**.
5. Todos os endpoints protegidos (`/api/categorias` e `/api/chamados`) estarão liberados para testes!