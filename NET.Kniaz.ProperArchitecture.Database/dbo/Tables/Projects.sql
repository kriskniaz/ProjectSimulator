CREATE TABLE [dbo].[Projects] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Name]        NVARCHAR (MAX)   NOT NULL,
    [Description] NVARCHAR (MAX)   NOT NULL,
    [StartWeek]   INT              NOT NULL,
    [EndWeek]     INT              NOT NULL,
    [StartYear]   INT              NOT NULL,
    [EndYear]     INT              NOT NULL,
    CONSTRAINT [PK_Projects_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);

