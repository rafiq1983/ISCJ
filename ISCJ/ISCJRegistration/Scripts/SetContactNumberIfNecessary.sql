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

print ('Setting contact numbers');

IF((SELECT COUNT(*) from sysobjects where name = 'SetContactNumberIfNecessary') >0)
BEGIN
   DROP PROCEDURE SetContactNumberIfNecessary
   
END
GO

CREATE Procedure SetContactNumberIfNecessary --creating SP as we don't have to worry variable conflicts.
AS
BEGIN

BEGIN TRANSACTION
DECLARE @contacts TABLE (rowNumber INT IDENTITY, ContactId UniqueIdentifier, TenantId UniqueIdentifier, ContactNumber int)

INSERT INTO @contacts(ContactId, TenantId, contactNumber)
SELECT [guid], TenantId, ContactNumber from Contacts;

DECLARE @count_SetContactNumber INT ; 
SELECT @count_SetContactNumber = COUNT(*) FROM @contacts;
DECLARE @index_ContactNumber INT =0;
DECLARE @tenantId UniqueIdentifier;
DECLARE @contactNumber INT;
DECLARE @ContactId UniqueIdentifier;

WHILE(@index_ContactNumber < @count_SetContactNumber)
BEGIN

SET @index_ContactNumber = @index_ContactNumber+1;

SELECT @ContactId = ContactId, @contactNumber = ContactNumber, @tenantId = TenantId 
from @contacts where rowNumber = @index_ContactNumber;

if(@contactNumber IS NULL or @contactNumber = 0)
 BEGIN
  
   DECLARE @contactCounter INT
   SELECT @contactCounter = CounterValue from SequenceCounter where TenantId=@TenantId and CounterName = 'ContactCounter';
   
  if(@contactCounter IS NOT NULL)
  BEGIN
   
   SET @contactCounter = @contactCounter + 1;
   UPDATE SequenceCounter SET CounterValue = @contactCounter where TenantId=@TenantId and CounterName = 'ContactCounter';
   UPDATE Contacts SET ContactNumber = @contactCounter Where [guid] = @ContactId;
   END

 END


END


 COMMIT TRANSACTION;
END

GO
EXEC SetContactNumberIfNecessary
GO
DROP Procedure SetContactNumberIfNecessary;
GO
print 'end of SetContactNumberIfNecessary';
