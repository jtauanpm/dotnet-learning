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
CREATE TABLE [Departamentos] (
    [Id] int NOT NULL IDENTITY,
    [Descricao] nvarchar(max) NOT NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Departamentos] PRIMARY KEY ([Id])
);

CREATE TABLE [Funcionarios] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NOT NULL,
    [CPF] nvarchar(max) NOT NULL,
    [DeparmentoId] int NOT NULL,
    [DepartamentoId] int NOT NULL,
    CONSTRAINT [PK_Funcionarios] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Funcionarios_Departamentos_DepartamentoId] FOREIGN KEY ([DepartamentoId]) REFERENCES [Departamentos] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_Funcionarios_DepartamentoId] ON [Funcionarios] ([DepartamentoId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250821012801_Initial', N'9.0.8');

ALTER TABLE [Funcionarios] ADD [RG] nvarchar(max) NOT NULL DEFAULT N'';

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250821012821_Add_Funcionario_RG', N'9.0.8');

ALTER TABLE [Departamentos] ADD [Excluido] bit NOT NULL DEFAULT CAST(0 AS bit);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250901013009_Add_Departamento_Excluido', N'9.0.8');

CREATE TABLE [Estados] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Estados] PRIMARY KEY ([Id])
);

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Nome') AND [object_id] = OBJECT_ID(N'[Estados]'))
    SET IDENTITY_INSERT [Estados] ON;
INSERT INTO [Estados] ([Id], [Nome])
VALUES (1, N'São Paulo'),
(2, N'Paraíba');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Nome') AND [object_id] = OBJECT_ID(N'[Estados]'))
    SET IDENTITY_INSERT [Estados] OFF;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250916011248_Add_Estados', N'9.0.8');

ALTER TABLE [Funcionarios] ADD [ContractType] nvarchar(max) NOT NULL DEFAULT N'';

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250917014757_Add_Funcionario_ContractType', N'9.0.8');

DECLARE @var sysname;
SELECT @var = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Funcionarios]') AND [c].[name] = N'DeparmentoId');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [Funcionarios] DROP CONSTRAINT [' + @var + '];');
ALTER TABLE [Funcionarios] DROP COLUMN [DeparmentoId];

ALTER TABLE [Funcionarios] ADD [Gender] nvarchar(1) NOT NULL DEFAULT N'';

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250918005646_Add_Funcionario_Gender', N'9.0.8');

COMMIT;
GO

