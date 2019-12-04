CREATE TABLE [dbo].[CreditCardPayment] (
    [PaymentId]           UNIQUEIDENTIFIER NOT NULL,
    [GatewayName]         VARCHAR (50)     NOT NULL,
	[Last4Digits]         Char(4)     NOT NULL,
	[CardBrand]            varchar(20) NOT NULL,
	[CardType]             varchar(20) NOT NULL,
	[PaymentNote] VARCHAR(500) NOT NULL,
	[AuthorizationCode] VARCHAR(50) NOT NULL, 
    [ConfirmationNumber] VARCHAR(50) NOT NULL, 
    [PayorId] UNIQUEIDENTIFIER NOT NULL, 
    [FinancialAccountId] UNIQUEIDENTIFIER NOT NULL, 
    [PaymentDate] DATETIME NOT NULL, 
    [PaymentAmount] DECIMAL NOT NULL, 
	[TenantId] UNIQUEIDENTIFIER NULL,
    [CreateDate]          DATETIME         NOT NULL,
    [CreateUser]          VARCHAR (50)     NOT NULL,
    [ModifiedDate]        DATETIME         NULL,
    [ModifiedUser]        VARCHAR (50)     NULL,
    [RowVersion]          ROWVERSION       NOT NULL, 

);

