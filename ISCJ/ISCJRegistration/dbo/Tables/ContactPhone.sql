CREATE TABLE [dbo].[ContactPhone] (
    [Guid]        UNIQUEIDENTIFIER NOT NULL,
    [ContactGuid] UNIQUEIDENTIFIER NOT NULL,
    [PhoneNumber] VARCHAR (30)     NOT NULL,
    [Description] VARCHAR (100)    NOT NULL, 
    [TenantId] UNIQUEIDENTIFIER NOT NULL
);

