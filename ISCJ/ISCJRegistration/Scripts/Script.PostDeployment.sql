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

INSERT INTO [dbo].[Rooms]
           ([RoomName]
           ,[TenantId]
           ,[CreateDate]
           ,[CreateUser]
           )
     VALUES
           ('Cube1',
		   '00000000-0000-0000-0000-000000000000',           
           '2019-09-22 02:39:13.507'
           ,'Rafiq'
           )
GO