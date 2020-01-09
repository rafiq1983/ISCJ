CREATE TABLE [dbo].[SequenceCounter]
(
	[CounterId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CounterType] VARCHAR(50) NOT NULL, 
    [TenantId] UNIQUEIDENTIFIER NULL, 
    [CounterValue] INT NOT NULL, 
    CONSTRAINT [CK_SequenceCounter_CounterType] CHECK (CounterType = 'Global' or CounterType = 'Tenant')
)
