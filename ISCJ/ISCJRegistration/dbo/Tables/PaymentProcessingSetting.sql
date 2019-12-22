CREATE TABLE [dbo].[PaymentProcessingSetting] (
    [PaymentProcessorSettingId] UNIQUEIDENTIFIER NOT NULL,
    [ProcessorType]             VARCHAR (50)     NOT NULL,
    [TenantId]                  UNIQUEIDENTIFIER NOT NULL,
    [RowVersion]                ROWVERSION       NOT NULL,
    [CreateDate]                DATETIME         NOT NULL,
    [CreateUser]                VARCHAR (50)     NOT NULL,
    [ModifiedDate]              DATETIME         NULL,
    [ModifiedUser]              VARCHAR (50)     NULL
);

