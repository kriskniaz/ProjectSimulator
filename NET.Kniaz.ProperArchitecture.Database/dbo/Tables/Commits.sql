CREATE TABLE [dbo].[Commits] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Description] NVARCHAR (MAX)   NOT NULL,
    [StoryId]     UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([StoryId]) REFERENCES [dbo].[Stories] ([Id])
);

