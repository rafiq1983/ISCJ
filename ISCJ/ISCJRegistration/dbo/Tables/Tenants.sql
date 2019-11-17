CREATE TABLE [dbo].[Tenants] (
    [TenantId]          UNIQUEIDENTIFIER NOT NULL,
    [TenantDescription] VARCHAR (500)    NOT NULL,
    [LogoId]            VARCHAR (500)    NULL,
    [DomainName]        VARCHAR (50)     NOT NULL,
    [CreateDate]        DATETIME         NOT NULL,
    [CreateUser]        VARCHAR (50)     NOT NULL,
    [ModifiedDate]      DATETIME         NULL,
    [ModifiedUser]      VARCHAR (50)     NULL,
    [RowVersion]        ROWVERSION       NOT NULL,
    [IsVerified] TINYINT NULL, 
    CONSTRAINT [PK_Tenants] PRIMARY KEY CLUSTERED ([TenantId] ASC)
);

