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