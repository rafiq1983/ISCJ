CREATE TABLE [dbo].[InvoiceTypes]
(
	[InvoiceTypeId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [TenantId] UNIQUEIDENTIFIER NOT NULL, 
    [InvoiceTypeCd] VARCHAR(20) NOT NULL, 
    [ShortDescription] VARCHAR(50) NOT NULL, 
    [CreateDate] DATETIME NOT NULL, 
    [CreateUser] VARCHAR(50) NOT NULL, 
    [ModifiedDate] DATETIME NULL, 
    [ModifiedUser] VARCHAR(50) NULL
)
