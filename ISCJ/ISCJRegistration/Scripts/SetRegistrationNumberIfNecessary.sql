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
print ('Setting registration numbers');

IF((SELECT COUNT(*) from sysobjects where name = 'SetRegistrationNumbersIfNecessary') >0)
BEGIN
   DROP PROCEDURE SetStudentNumbersIfNecessary
   
END
GO

CREATE Procedure SetRegistrationNumbersIfNecessary --creating SP as we don't have to worry variable conflicts.
AS
BEGIN

BEGIN TRANSACTION
DECLARE @registrations TABLE (rowNumber INT IDENTITY, registrationId UniqueIdentifier, TenantId UniqueIdentifier, registrationNumber int)

INSERT INTO @registrations(registrationId, TenantId, registrationNumber)
SELECT ApplicationId, TenantId, ApplicationNumber from RegistrationApplication;

DECLARE @count INT ; 
SELECT @count = COUNT(*) FROM @registrations;
DECLARE @index INT =0;
DECLARE @tenantId UniqueIdentifier;
DECLARE @registrationNumber INT;
DECLARE @registrationId UniqueIdentifier;

WHILE(@index < @count)
BEGIN

SET @index = @index+1;

SELECT @registrationId = registrationId, @registrationNumber = registrationNumber, @tenantId = TenantId 
from @registrations where rowNumber = @index;

if(@registrationNumber IS NULL or @registrationNumber = 0)
 BEGIN
  
   DECLARE @registrationCounter INT
   SELECT @registrationCounter = CounterValue from SequenceCounter where TenantId=@TenantId and CounterName = 'RegistrationApplicationCounter';
   SET @registrationCounter = @registrationCounter + 1;
   UPDATE SequenceCounter SET CounterValue = @registrationCounter where TenantId=@TenantId and CounterName = 'RegistrationApplicationCounter';
   UPDATE RegistrationApplication SET ApplicationNumber = @registrationCounter Where ApplicationId = @registrationId;

 END


END


 COMMIT TRANSACTION;
END

GO
EXEC SetRegistrationNumbersIfNecessary
GO
DROP Procedure SetRegistrationNumbersIfNecessary;
GO
print 'end of SetRegistrationNumbersIfNecessary';