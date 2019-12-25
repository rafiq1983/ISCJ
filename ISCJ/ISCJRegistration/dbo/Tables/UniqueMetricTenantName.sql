ALTER TABLE [dbo].[Metrics]
	ADD CONSTRAINT [UniqueMetricTenantName]
	UNIQUE (MetricName,TenantId)
