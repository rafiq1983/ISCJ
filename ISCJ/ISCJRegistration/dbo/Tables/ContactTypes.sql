CREATE TABLE [dbo].[ContactTypes] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_ContactTypes] PRIMARY KEY CLUSTERED ([ID] ASC)
);

