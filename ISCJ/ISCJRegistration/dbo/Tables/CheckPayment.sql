CREATE TABLE [dbo].[CheckPayment] (
    [PaymentId]          UNIQUEIDENTIFIER NOT NULL,
    [CheckNumber]        VARCHAR (50)     NOT NULL,
    [CheckDate]          DATE             NOT NULL,
    [NameOnCheck]        VARCHAR (100)    NOT NULL,
    [CheckAccountNumber] VARCHAR (50)     NOT NULL,
    [CheckBankName]      VARCHAR (50)     NOT NULL,
    [RowVersion]         ROWVERSION       NOT NULL,
    [CheckImage]         VARBINARY (MAX)  NULL,
    [PayerId]            UNIQUEIDENTIFIER NULL,
    [PaymentDate]        DATETIME         NOT NULL,
    [PaymentAmount]      DECIMAL (18)     NOT NULL,
    [FinancialAccountId] UNIQUEIDENTIFIER NOT NULL,
    [InvoiceId]          UNIQUEIDENTIFIER NULL,
    [IsCleared]          TINYINT          CONSTRAINT [DF_CheckPayment_IsCleared] DEFAULT ((0)) NOT NULL,
    [CreateDate]         DATETIME         NOT NULL,
    [CreateUser]         VARCHAR (50)     NOT NULL,
    [ModifiedDate]       DATETIME         NULL,
    [ModifiedUser]       DATETIME         NULL
);

