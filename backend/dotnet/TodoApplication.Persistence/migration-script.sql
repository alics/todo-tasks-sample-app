-- Execute this script in a database named Todo.

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

CREATE TABLE [Tasks] (
    [Id] BigInt NOT NULL,
    [Title] nvarchar(200) NOT NULL,
    [CreationDate] DateTime NOT NULL,
    [DeadlineDate] DateTime NOT NULL,
    [Status] TinyInt NOT NULL,
    CONSTRAINT [PK_Tasks] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [TaskHistories] (
    [Id] int NOT NULL IDENTITY,
    [TaskId] BigInt NOT NULL,
    [TaskStatus] TinyInt NOT NULL,
    [DateTime] DateTime NOT NULL,
    CONSTRAINT [PK_TaskHistories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TaskHistories_Tasks_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [Tasks] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_TaskHistories_TaskId] ON [TaskHistories] ([TaskId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240406161003_InitDb', N'8.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [TaskHistories] DROP CONSTRAINT [FK_TaskHistories_Tasks_TaskId];
GO

DROP TABLE [Tasks];
GO

CREATE TABLE [TodoTasks] (
    [Id] BigInt NOT NULL,
    [Title] nvarchar(20) NOT NULL,
    [CreationDate] DateTime NOT NULL,
    [DeadlineDate] DateTime NOT NULL,
    [Status] TinyInt NOT NULL,
    CONSTRAINT [PK_TodoTasks] PRIMARY KEY ([Id])
);
GO

ALTER TABLE [TaskHistories] ADD CONSTRAINT [FK_TaskHistories_TodoTasks_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [TodoTasks] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240406175943_RenameTaskTable', N'8.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[TodoTasks].[DeadlineDate]', N'Deadline', N'COLUMN';
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TodoTasks]') AND [c].[name] = N'Title');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [TodoTasks] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [TodoTasks] ALTER COLUMN [Title] nvarchar(500) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240408081130_ChangeTaskTitleFieldSize', N'8.0.3');
GO

COMMIT;
GO

