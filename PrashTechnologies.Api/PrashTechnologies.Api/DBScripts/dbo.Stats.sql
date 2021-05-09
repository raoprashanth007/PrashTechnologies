CREATE TABLE [dbo].[Stats] (
    [Id]          INT              IDENTITY (1, 1) NOT NULL,
    [ProductCode] UNIQUEIDENTIFIER NOT NULL,
    [Clicks]      BIGINT           DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([ProductCode] ASC)
);

