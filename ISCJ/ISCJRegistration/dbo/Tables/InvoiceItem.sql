CREATE TABLE [dbo].[InvoiceItem] (
    [ItemId]       UNIQUEIDENTIFIER NOT NULL,
    [InvoiceId]    UNIQUEIDENTIFIER NOT NULL,
    [Amount]       DECIMAL (18)     NULL,
    [SalesTax]     DECIMAL (18)     NULL,
    [Description]  VARCHAR (500)    NOT NULL,
    [Quantity]     INT              NOT NULL,
    [CreateUser]   VARCHAR (50)     NOT NULL,
    [CreateDate]   DATETIME         NOT NULL,
    [ModifiedUser] VARCHAR (50)     NULL,
    [ModifiedDate] DATETIME         NULL,
    [RowVersion]   ROWVERSION       NOT NULL,
    [TenantId] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [PK_InvoiceItem] PRIMARY KEY CLUSTERED ([ItemId] ASC)
);

