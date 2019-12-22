ALTER TABLE [dbo].[BillableProduct]
	ADD CONSTRAINT [IX_BillableProducts]
	UNIQUE (ProductCode ASC, TenantId ASC)
