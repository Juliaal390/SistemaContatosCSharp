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

CREATE TABLE [Contatos] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Celular] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Contatos] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240825014900_CriandoTabelaContatos', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Usuarios] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NOT NULL,
    [Login] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Perfil] int NOT NULL,
    [Senha] nvarchar(max) NOT NULL,
    [DataCadastro] datetime2 NOT NULL,
    [DataAtualizacao] datetime2 NULL,
    CONSTRAINT [PK_Usuarios] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240831142024_CriacaoTabelaUsuario', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Contatos] ADD [UsuarioId] int NULL;
GO

CREATE INDEX [IX_Contatos_UsuarioId] ON [Contatos] ([UsuarioId]);
GO

ALTER TABLE [Contatos] ADD CONSTRAINT [FK_Contatos_Usuarios_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuarios] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240914194551_CriandoVinculoUsuarioContatos', N'8.0.8');
GO

COMMIT;
GO

