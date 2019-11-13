CREATE TABLE [dbo].[CashPayment] (
    [PaymentId]          UNIQUEIDENTIFIER NOT NULL,
    [PaymentDate]        DATETIME         NOT NULL,
    [PaymentAmount]      DECIMAL (18)     NOT NULL,
    [PayerId]            UNIQUEIDENTIFIER NOT NULL,
    [FinancialAccountId] UNIQUEIDENTIFIER NOT NULL,
    [InvoiceId]          UNIQUEIDENTIFIER NULL,
    [CreateDate]         DATETIME         NOT NULL,
    [CreateUser]         VARCHAR (50)     NOT NULL,
    [ModifiedDate]       DATETIME         NULL,
    [ModifiedUser]       VARCHAR (50)     NULL,
    [RowVersion]         ROWVERSION       NOT NULL
);

