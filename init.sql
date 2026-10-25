BEGIN TRANSACTION;
GO

CREATE TABLE [Orders] (
    [Id] uniqueidentifier NOT NULL,
    [Date] datetime2 NOT NULL,
    [TotalPrice] decimal(18,2) NOT NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Products] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [Stock] int NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [OrderDetails] (
    [OrderId] uniqueidentifier NOT NULL,
    [ProductId] int NOT NULL,
    [Quantity] int NOT NULL,
    [UnitPrice] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_OrderDetails] PRIMARY KEY ([OrderId], [ProductId]),
    CONSTRAINT [FK_OrderDetails_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_OrderDetails_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_OrderDetails_ProductId] ON [OrderDetails] ([ProductId]);
GO

BEGIN TRANSACTION;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'Price', N'Stock') AND [object_id] = OBJECT_ID(N'[Products]'))
    SET IDENTITY_INSERT [Products] ON;
INSERT INTO [Products] ([Id], [Name], [Price], [Stock])
VALUES (1, N'Laptop Lenovo P14S 2da Gen', 35000.0, 10),
(2, N'Mouse Logitech G Pro', 6700.0, 15),
(3, N'Teclado Mecanico Krom Kasic TKL', 16000.0, 15);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'Price', N'Stock') AND [object_id] = OBJECT_ID(N'[Products]'))
    SET IDENTITY_INSERT [Products] OFF;
GO
COMMIT;

BEGIN TRANSACTION;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Date', N'Status', N'TotalPrice') AND [object_id] = OBJECT_ID(N'[Orders]'))
    SET IDENTITY_INSERT [Orders] ON;
INSERT INTO [Orders] ([Id], [Date], [Status], [TotalPrice])
VALUES ('51449123-eede-4982-aa0c-7a618346b174', '2026-10-25T13:15:54.1836181Z', 0, 10000.0);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Date', N'Status', N'TotalPrice') AND [object_id] = OBJECT_ID(N'[Orders]'))
    SET IDENTITY_INSERT [Orders] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'Price', N'Stock') AND [object_id] = OBJECT_ID(N'[Products]'))
    SET IDENTITY_INSERT [Products] ON;
INSERT INTO [Products] ([Id], [Name], [Price], [Stock])
VALUES (4, N'Mouse Inalambrico Logitech', 5000.0, 13);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'Price', N'Stock') AND [object_id] = OBJECT_ID(N'[Products]'))
    SET IDENTITY_INSERT [Products] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'OrderId', N'ProductId', N'Quantity', N'UnitPrice') AND [object_id] = OBJECT_ID(N'[OrderDetails]'))
    SET IDENTITY_INSERT [OrderDetails] ON;
INSERT INTO [OrderDetails] ([OrderId], [ProductId], [Quantity], [UnitPrice])
VALUES ('51449123-eede-4982-aa0c-7a618346b174', 4, 2, 5000.0);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'OrderId', N'ProductId', N'Quantity', N'UnitPrice') AND [object_id] = OBJECT_ID(N'[OrderDetails]'))
    SET IDENTITY_INSERT [OrderDetails] OFF;
GO

COMMIT;