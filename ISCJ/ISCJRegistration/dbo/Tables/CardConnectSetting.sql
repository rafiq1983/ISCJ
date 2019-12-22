CREATE TABLE [dbo].[CardConnectSetting] (
    [CardConnectSettingId] UNIQUEIDENTIFIER NOT NULL,
    [MerchantId]           UNIQUEIDENTIFIER NOT NULL,
    [GatewayUrl]           VARCHAR (1000)   NOT NULL,
    [CreateDate]           DATETIME         NOT NULL,
    [CreateUser]           VARCHAR (50)     NOT NULL,
    [ModifiedDate]         DATETIME         NULL,
    [ModifiedUser]         VARCHAR (50)     NULL,
    [RowVersion]           ROWVERSION       NOT NULL, 
    [TenantId] UNIQUEIDENTIFIER NOT NULL
);

