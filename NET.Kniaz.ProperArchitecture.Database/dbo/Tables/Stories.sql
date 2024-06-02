CREATE TABLE [dbo].[Stories] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [PointValue]  INT              NULL,
    [Description] NVARCHAR (MAX)   NOT NULL,
    [SprintId]    UNIQUEIDENTIFIER NULL,
    [IsDone]      INT              DEFAULT ((0)) NULL,
    [ProjectId]   UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([Id]),
    FOREIGN KEY ([SprintId]) REFERENCES [dbo].[Sprints] ([Id])
);

