CREATE TABLE [dbo].[TeamMembers] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [ResourceId] UNIQUEIDENTIFIER NOT NULL,
    [TeamId]     UNIQUEIDENTIFIER NOT NULL,
    [StartWeek]  INT              NOT NULL,
    [EndWeek]    INT              NOT NULL,
    [StartYear]  INT              NOT NULL,
    [EndYear]    INT              NOT NULL,
    [HourlyRate] DECIMAL (10, 2)  NOT NULL,
    [CurrencyId] UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Currencies] ([Id]),
    FOREIGN KEY ([ResourceId]) REFERENCES [dbo].[Resources] ([Id]),
    FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Teams] ([Id])
);

