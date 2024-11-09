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

CREATE TABLE [CategoryFoods] (
    [Id] uniqueidentifier NOT NULL,
    [CategoryFoodName] nvarchar(100) NOT NULL,
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
    CONSTRAINT [PK_CategoryFoods] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Orders] (
    [Id] uniqueidentifier NOT NULL,
    [OrderCode] nvarchar(450) NOT NULL,
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
    CONSTRAINT [PK_Orders] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Restaurants] (
    [Id] uniqueidentifier NOT NULL,
    [RestaurantName] nvarchar(100) NOT NULL,
    [RestaurantAddress] nvarchar(150) NOT NULL,
    [RestaurantLinkImage] nvarchar(max) NOT NULL,
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
    CONSTRAINT [PK_Restaurants] PRIMARY KEY ([Id])
);
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

CREATE TABLE [Foods] (
    [Id] uniqueidentifier NOT NULL,
    [RestaurantId] uniqueidentifier NOT NULL,
    [CategoryFoodId] uniqueidentifier NOT NULL,
    [FoodName] nvarchar(100) NOT NULL,
    [FoodPrice] nvarchar(100) NOT NULL,
    [FoodLinkImage] nvarchar(max) NOT NULL,
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
    CONSTRAINT [PK_Foods] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Foods_CategoryFoods_CategoryFoodId] FOREIGN KEY ([CategoryFoodId]) REFERENCES [CategoryFoods] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Foods_Restaurants_RestaurantId] FOREIGN KEY ([RestaurantId]) REFERENCES [Restaurants] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [OrderDetails] (
    [Id] uniqueidentifier NOT NULL,
    [FoodId] uniqueidentifier NOT NULL,
    [OrderId] uniqueidentifier NOT NULL,
    [userId] uniqueidentifier NOT NULL,
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
    CONSTRAINT [PK_OrderDetails] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OrderDetails_Foods_FoodId] FOREIGN KEY ([FoodId]) REFERENCES [Foods] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_OrderDetails_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_OrderDetails_Users_userId] FOREIGN KEY ([userId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Foods_CategoryFoodId] ON [Foods] ([CategoryFoodId]);
GO

CREATE INDEX [IX_Foods_RestaurantId] ON [Foods] ([RestaurantId]);
GO

CREATE INDEX [IX_OrderDetails_FoodId] ON [OrderDetails] ([FoodId]);
GO

CREATE INDEX [IX_OrderDetails_OrderId] ON [OrderDetails] ([OrderId]);
GO

CREATE INDEX [IX_OrderDetails_userId] ON [OrderDetails] ([userId]);
GO

CREATE UNIQUE INDEX [IX_Orders_OrderCode] ON [Orders] ([OrderCode]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241025080827_25102024_CreateDataBase_V1', N'8.0.10');
GO

COMMIT;
GO

