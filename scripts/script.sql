CREATE DATABASE CoreDeskDb;
GO

USE CoreDeskDb;
GO

CREATE TABLE Categories (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);

CREATE TABLE Tickets (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(150) NOT NULL,
    Description NVARCHAR(MAX) NOT NULL,
    Priority INT NOT NULL,
    Status INT NOT NULL,
    RequesterName NVARCHAR(100) NOT NULL,
    CreatedAt DATETIME2 NOT NULL,
    ClosedAt DATETIME2 NULL,
    Solution NVARCHAR(MAX) NULL,
    CategoryId INT NOT NULL,
    CONSTRAINT FK_Tickets_Categories FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
);

CREATE TABLE TicketInteractions (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TicketId INT NOT NULL,
    Author NVARCHAR(100) NOT NULL,
    Message NVARCHAR(MAX) NOT NULL,
    RegisteredAt DATETIME2 NOT NULL,
    CONSTRAINT FK_TicketInteractions_Tickets FOREIGN KEY (TicketId) REFERENCES Tickets(Id) ON DELETE CASCADE
);

INSERT INTO Categories (Name) VALUES ('Hardware'), ('Software'), ('Network / Connectivity'), ('Access & Permissions');