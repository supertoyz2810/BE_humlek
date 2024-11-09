IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Users] (
    [Id] uniqueidentifier NOT NULL,
    [UserName] nvarchar(100) NOT NULL,
    [UserPassword] nvarchar(max) NOT NULL,
    [Note] nvarchar(max) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedUser] uniqueidentifier NOT NULL,
    [CreatedName] nvarchar(255) NOT NULL,
    [UpdatedDate] datetime2 NOT NULL,
    [UpdatedUser] uniqueidentifier NOT NULL,
    [UpdatedName] nvarchar(255) NOT NULL,
    [DeletedDate] datetime2 NULL,
    [DeletedUser] uniqueidentifier NULL,
    [DeletedName] nvarchar(255) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241025064735_25102024_CreateDataBase_V1', N'8.0.10');
GO

COMMIT;
GO

