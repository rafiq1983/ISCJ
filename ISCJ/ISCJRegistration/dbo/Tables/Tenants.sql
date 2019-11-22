CREATE TABLE [dbo].[Tenants] (
    [TenantId]          UNIQUEIDENTIFIER NOT NULL,
    [OrganizationName] VARCHAR (500)    NOT NULL,
    [LogoId]            VARCHAR (500)    NULL,
    [CreateDate]        DATETIME         NOT NULL,
    [CreateUser]        VARCHAR (50)     NOT NULL,
    [ModifiedDate]      DATETIME         NULL,
    [ModifiedUser]      VARCHAR (50)     NULL,
    [RowVersion]        ROWVERSION       NOT NULL,
    [IsVerified] TINYINT NULL, 
    [OwernId] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [PK_Tenants] PRIMARY KEY CLUSTERED ([TenantId] ASC)
);

