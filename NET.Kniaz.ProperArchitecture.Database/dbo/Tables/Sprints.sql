CREATE TABLE [dbo].[Sprints] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [ProjectId]       UNIQUEIDENTIFIER NOT NULL,
    [TeamId]          UNIQUEIDENTIFIER NOT NULL,
    [StartWeek]       INT              NOT NULL,
    [EndWeek]         INT              NOT NULL,
    [committedpoints] INT              NULL,
    [DeliveredPoints] INT              NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([Id]),
    FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Teams] ([Id])
);

