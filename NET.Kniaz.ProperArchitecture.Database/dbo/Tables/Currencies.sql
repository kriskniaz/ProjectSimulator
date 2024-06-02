CREATE TABLE [dbo].[Currencies] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [Name]       NVARCHAR (MAX)   NOT NULL,
    [ShortName]  NVARCHAR (MAX)   NOT NULL,
    [ValueinUSD] DECIMAL (18, 2)  NOT NULL,
    CONSTRAINT [PK_Currencys_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);

