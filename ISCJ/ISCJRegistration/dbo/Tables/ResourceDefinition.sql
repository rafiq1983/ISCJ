CREATE TABLE [dbo].[ResourceDefinition]
(
	[Id] INT IDENTITY NOT NULL PRIMARY KEY, 
    [SectionName] VARCHAR(50) NOT NULL, 	
    [ResourceName] VARCHAR(50) NOT NULL, 
    [ValueType] VARCHAR(50) NOT NULL, 
    CONSTRAINT [CK_ResourceDefinition_ValidValueTypes] CHECK (SectionName='String' or SectionName='Image')
)
