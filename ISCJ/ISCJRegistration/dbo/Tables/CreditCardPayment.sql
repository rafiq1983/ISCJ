CREATE TABLE [dbo].[CreditCardPayment] (
    [PaymentId]           UNIQUEIDENTIFIER NOT NULL,
    [AuthorizationNumber] VARCHAR (100)    NOT NULL,
    [GatewayName]         VARCHAR (50)     NOT NULL,
    [CreateDate]          DATETIME         NOT NULL,
    [CreateUser]          VARCHAR (50)     NOT NULL,
    [ModifiedDate]        DATETIME         NULL,
    [ModifiedUser]        VARCHAR (50)     NULL,
    [RowVersion]          ROWVERSION       NOT NULL
);

