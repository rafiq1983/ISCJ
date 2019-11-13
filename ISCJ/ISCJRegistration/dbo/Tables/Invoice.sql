CREATE TABLE [dbo].[Invoice] (
    [InvoiceId]          UNIQUEIDENTIFIER NOT NULL,
    [InvoiceAmount]      DECIMAL (18)     NOT NULL,
    [TennantId]          UNIQUEIDENTIFIER NOT NULL,
    [FinancialAccountId] UNIQUEIDENTIFIER NOT NULL,
    [DueDate]            DATE             NOT NULL,
    [Description]        VARCHAR (500)    NOT NULL,
    [GenerationDate]     DATE             NOT NULL,
    [IsPaid]             BIT              CONSTRAINT [DF__Invoice__IsPaid__6B24EA82] DEFAULT ((0)) NOT NULL,
    [TotalPaid]          DECIMAL (18)     NULL,
    [OrderId]            VARCHAR (50)     NOT NULL,
    [OrderType]          VARCHAR (50)     NOT NULL,
    [CreateUser]         VARCHAR (50)     NOT NULL,
    [CreateDate]         DATETIME         NOT NULL,
    [ModifiedUser]       VARCHAR (50)     NULL,
    [ModifiedDate]       DATETIME         NULL,
    [RowVersion]         ROWVERSION       NOT NULL,
    CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED ([InvoiceId] ASC)
);

