CREATE TABLE [dbo].[CreditCardPayment] (
    [PaymentId]           UNIQUEIDENTIFIER NOT NULL,
    [AuthorizationNumber] VARCHAR (100)    NOT NULL,
    [GatewayName]         VARCHAR (50)     NOT NULL,
	[Last4Digits]         Char(4)     NOT NULL,
	[CardBrand]            varchar(20) NOT NULL,
	[CardType]             varchar(20) NOT NULL,
    [CreateDate]          DATETIME         NOT NULL,
    [CreateUser]          VARCHAR (50)     NOT NULL,
    [ModifiedDate]        DATETIME         NULL,
    [ModifiedUser]        VARCHAR (50)     NULL,
    [RowVersion]          ROWVERSION       NOT NULL, 
    [TenantId] UNIQUEIDENTIFIER NULL,
	[PaymentNote] VARCHAR(500) NOT NULL
);

