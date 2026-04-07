CREATE TABLE [dbo].[Resources] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [FirstName] NVARCHAR (MAX)   NOT NULL,
    [LastName]  NVARCHAR (MAX)   NOT NULL,
    [Title]     NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_Resources_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);

