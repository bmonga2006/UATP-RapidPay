CREATE DATABASE [RapidPay]
GO

USE RapidPay
GO

CREATE TABLE dbo.cards (
    id INT IDENTITY(1,1) PRIMARY KEY,
    cardNumber NVARCHAR(50) NOT NULL,
    balance DECIMAL(18, 2) NOT NULL,
    last4digits NVARCHAR(4) NOT NULL,
    dateCreated DATETIME not NULL default getdate(),
    dateUpdated DATETIME not NULL default getdate()
);
GO

CREATE NONCLUSTERED INDEX IX_cards_cardNumber ON dbo.cards (cardNumber) INCLUDE (balance, last4digits);
GO

CREATE TABLE dbo.transactions
(
 id INT IDENTITY(1,1) PRIMARY KEY,
 cardId INT NOT NULL,
 transactionAmount DECIMAL(18, 2) NOT NULL,
 transactionFee DECIMAL(2,2) NOT NULL,
 dateCreated DATETIME NOT NULL default getdate(),
 CONSTRAINT FK_cardId FOREIGN KEY (cardId) REFERENCES dbo.cards(id)
 )
 GO

CREATE NONCLUSTERED INDEX IX_transactions_cardId ON dbo.transactions (cardId) INCLUDE (transactionAmount, transactionFee, dateCreated );
GO