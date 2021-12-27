CREATE DATABASE [Shop];
GO

USE [Shop];

CREATE TABLE [dbo].[Categories]
(
	[id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[name] NVARCHAR(100) NOT NULL
);

CREATE TABLE [dbo].[Products]
(
	[id]INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[name] NVARCHAR(255) NOT NULL,
	[price] DECIMAL(10, 2) NOT NULL,
	[categoryId] INT NOT NULL REFERENCES [dbo].[categories]([id])
);
GO

INSERT INTO [dbo].[Categories]([name])
VALUES (N'Электроника'), (N'Продукты питания'), (N'Бытовая химия'), (N'Сад');

INSERT INTO [dbo].[Products]([name], [price], [categoryId])
VALUES 	(N'газонокосилка HN-1', 15250.50, 4), (N'наушники HD569', 7250.00, 1), (N'отбеливатель Tide', 95.50, 3), 
		(N'газонокосилка HN-2', 17500.50, 4), (N'сыр Бри', 1250.99, 2), (N'порошок Tide', 390.50, 3);
GO