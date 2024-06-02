CREATE TABLE [dbo].[SimulationResults] (
    [Id]                      UNIQUEIDENTIFIER NOT NULL,
    [RunDate]                 DATETIME         NOT NULL,
    [DeliveredPoints]         INT              NOT NULL,
    [CommitedPoints]          INT              NOT NULL,
    [NumberOfPlannedSprints]  INT              NOT NULL,
    [NumberOfExecutedSprints] INT              NOT NULL,
    [SimulatorType]           NVARCHAR (MAX)   NOT NULL,
    [NumberOfDeveloperEvents] INT              NULL,
    [DeveloperEventsImpact]   INT              NULL,
    [NumberOfScopeEvents]     INT              NULL,
    [ScopeEventsImpact]       INT              NULL,
    CONSTRAINT [PK_SimulationResult] PRIMARY KEY CLUSTERED ([Id] ASC),
    CHECK ([CommitedPoints]>=(0)),
    CHECK ([DeliveredPoints]>=(0))
);

