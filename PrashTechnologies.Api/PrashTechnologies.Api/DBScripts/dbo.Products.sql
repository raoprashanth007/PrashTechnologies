CREATE TABLE [dbo].[Products] (
    [Id]           INT              IDENTITY (1, 1) NOT NULL,
    [Code]         UNIQUEIDENTIFIER NOT NULL,
    [Name]         NVARCHAR (20)    NOT NULL,
    [Description]  NVARCHAR (50)    NULL,
    [CurrentCost]  MONEY            NOT NULL,
    [ExchangeCode] CHAR (10)        DEFAULT ('USD') NOT NULL,
    [IsActive]     BIT              DEFAULT ((1)) NOT NULL,
    [CreatedBy]    NCHAR (20)       DEFAULT ('prash technologies') NOT NULL,
    [CreatedOn]    DATETIME         DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

