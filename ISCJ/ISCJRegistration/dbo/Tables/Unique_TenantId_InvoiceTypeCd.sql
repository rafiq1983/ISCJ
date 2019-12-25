ALTER TABLE [dbo].[InvoiceTypes]
	ADD CONSTRAINT [Unique_TenantId_InvoiceTypeCd]
	UNIQUE (TenantId, [InvoiceTypeName])
