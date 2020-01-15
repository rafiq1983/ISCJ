/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
IF NOT EXISTS (SELECT * FROM ResourceDefinition)
BEGIN
Declare @tbl table(id int identity, sectionName varchar(50), keyName varchar(50), ValueType varchar(50))
INSERT INTO @tbl values('FieldLabels', 'ApplicationNumber', 'String');

Declare @count INT
select @count = count(*) from @tbl;

Declare @index_addresourcedefinitions INT = 1;
Declare @sectionName varchar(50), @keyName varchar(50), @ValueType varchar(50);

while(@index_addresourcedefinitions <= @count)
BEGIN
  IF NOT EXISTS(SELECT COUNT(*) FROM ResourceDefinition where SectionName = @sectionName and ResourceName = @keyName)
   BEGIN
      SELECT @sectionName = SectionName, @KeyName = keyName, @ValueType = valueType from @tbl where id=@index_addresourcedefinitions;

      INSERT INTO ResourceDefinition(SectionName, ResourceName,ValueType)
	  values(@sectionName, @keyName,@valueType);

   END
END

INSERT INTO [dbo].[ResourceDefinition]
           ([Description]
        
           )
     VALUES
           ('Student'
           )

		   INSERT INTO [dbo].[ContactTypes]
           ([Description]
        
           )
     VALUES
           ('Parent'
           )

		   INSERT INTO [dbo].[ContactTypes]
           ([Description]
        
           )
     VALUES
           ('Other'
           )
		END
GO