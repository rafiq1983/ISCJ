CREATE TABLE [dbo].[BillableProduct] (
    [ProductId]      UNIQUEIDENTIFIER NOT NULL,
    [Price]          DECIMAL (18)     NOT NULL,
    [Description]    NVARCHAR (500)   NOT NULL,
    [EffectiveDate]  DATETIME         NOT NULL,
    [ExpirationDate] DATETIME         NOT NULL,
    [ProductCode]    VARCHAR (50)     NOT NULL,
    [IsActive]       TINYINT          NOT NULL,
    [CreateDate]     DATETIME         NOT NULL,
    [CreateUser]     VARCHAR (50)     NOT NULL,
    [ModifiedDate]   DATETIME         NULL,
    [ModifiedUser]   VARCHAR (50)     NULL, 
    [TenantId] UNIQUEIDENTIFIER NOT NULL
);


GO
