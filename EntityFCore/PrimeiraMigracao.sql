﻿IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
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

CREATE TABLE [Cliente] (
    [Id] int NOT NULL IDENTITY,
    [Nome] VARCHAR(80) NOT NULL,
    [Telefone] CHAR(11) NULL,
    [CEP] CHAR(8) NOT NULL,
    [Estado] CHAR(2) NOT NULL,
    [Cidade] nvarchar(60) NOT NULL,
    CONSTRAINT [PK_Cliente] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Produtos] (
    [Id] int NOT NULL IDENTITY,
    [CodigoBarras] VARCHAR(14) NOT NULL,
    [Descricao] VARCHAR(60) NULL,
    [Valor] nvarchar(max) NOT NULL,
    [TipoProduto] nvarchar(max) NOT NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Produtos] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Pedidos] (
    [Id] int NOT NULL IDENTITY,
    [ClienteId] int NOT NULL,
    [IniciadoEm] datetime2 NOT NULL DEFAULT (GETDATE()),
    [FinalizadoEm] datetime2 NOT NULL,
    [TipoFrete] int NOT NULL,
    [StatusPedido] nvarchar(max) NOT NULL,
    [Observacao] VARCHAR(512) NULL,
    CONSTRAINT [PK_Pedidos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Pedidos_Cliente_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Cliente] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [PedidoItens] (
    [Id] int NOT NULL IDENTITY,
    [PedidoId] int NOT NULL,
    [ProdutoId] int NOT NULL,
    [Quantidade] int NOT NULL DEFAULT 1,
    [Valor] decimal(18,2) NOT NULL,
    [Desconto] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_PedidoItens] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PedidoItens_Pedidos_PedidoId] FOREIGN KEY ([PedidoId]) REFERENCES [Pedidos] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [idx_cliente_telefone] ON [Cliente] ([Telefone]);
GO

CREATE INDEX [IX_PedidoItens_PedidoId] ON [PedidoItens] ([PedidoId]);
GO

CREATE INDEX [IX_Pedidos_ClienteId] ON [Pedidos] ([ClienteId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210819213529_PrimeiraMigracao', N'5.0.9');
GO

COMMIT;
GO

