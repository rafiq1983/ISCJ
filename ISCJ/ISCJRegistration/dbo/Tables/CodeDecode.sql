
CREATE TABLE [dbo].[CodeDecode]
(
    [Id] UniqueIdentifier NOT NULL,
	[TenantId] UNIQUEIDENTIFIER NULL,
    [Code] VARCHAR(50) NOT NULL, 
    [Locale] CHAR(5) NOT NULL DEFAULT 'en-US', 
    [ShortDescription] VARCHAR(100) NOT NULL, 
    [LongDescription] VARCHAR(500) NOT NULL, 
    CONSTRAINT [PK_CodeDecode] PRIMARY KEY ([Id])
)
