
USE [master]

IF db_id('ShoeCollection') IS NULl
  CREATE DATABASE [ShoeCollection]
GO

USE [ShoeCollection]
GO


DROP TABLE IF EXISTS [Favorite];
DROP TABLE IF EXISTS [Shoe];
DROP TABLE IF EXISTS [Brand];
DROP TABLE IF EXISTS [Style];
DROP TABLE IF EXISTS [UserProfile];

GO

CREATE TABLE [Shoe] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [UserId] int NOT NULL,
  [Size] int NOT NULL,
  [StyleId] int NOT NULL,
  [BrandId] int NOT NULL,
  [ImageUrl] nvarchar(255)
)
GO

CREATE TABLE [User] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [FirstName] nvarchar(255) NOT NULL,
  [LastName] nvarchar(255) NOT NULL,
  [FirebaseUserId] nvarchar(255) NOT NULL,
  [Email] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [Brand] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [BrandName] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [Style] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [Favorite] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [UserId] int NOT NULL,
  [ShoeId] int NOT NULL
)
GO

ALTER TABLE [Shoe] ADD FOREIGN KEY ([UserId]) REFERENCES [User] ([Id])
GO

ALTER TABLE [Shoe] ADD FOREIGN KEY ([StyleId]) REFERENCES [Style] ([Id])
GO

ALTER TABLE [Shoe] ADD FOREIGN KEY ([BrandId]) REFERENCES [Brand] ([Id])
GO

ALTER TABLE [Favorite] ADD FOREIGN KEY ([UserId]) REFERENCES [User] ([Id])
GO

ALTER TABLE [Favorite] ADD FOREIGN KEY ([ShoeId]) REFERENCES [Shoe] ([Id]) ON DELETE CASCADE
GO
