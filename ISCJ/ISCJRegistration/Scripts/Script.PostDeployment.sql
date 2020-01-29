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


Print ('Post Deployment Script')

IF NOT EXISTS (SELECT * FROM ContactTypes)
BEGIN
INSERT INTO [dbo].[ContactTypes]
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

--Define Counters for organizations which are created previously but have no Counters.
:r .\DefineBuiltInCountersForExistingOrganizations.sql


--Set Student Counter values.
:r .\SetStudentNumbersIfNecessary.sql

:r .\SetRegistrationNumberIfNecessary.sql

:r .\SetContactNumberIfNecessary.sql