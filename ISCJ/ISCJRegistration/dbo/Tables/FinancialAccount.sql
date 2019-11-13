CREATE TABLE [dbo].[FinancialAccount] (
    [FinancialAccountId]   UNIQUEIDENTIFIER NOT NULL,
    [FinancialAccountType] VARCHAR (50)     NOT NULL,
    [FinancialAccountName] VARCHAR (100)    NOT NULL,
    [AccountNumber]        VARCHAR (50)     NULL,
    [TenantId]             UNIQUEIDENTIFIER NOT NULL,
    [CreateDate]           DATETIME         NOT NULL,
    [CreateUser]           VARCHAR (50)     NOT NULL,
    [ModifiedDate]         DATETIME         NULL,
    [ModifiedUser]         VARCHAR (50)     NULL,
    [RowVersion]           ROWVERSION       NOT NULL
);

