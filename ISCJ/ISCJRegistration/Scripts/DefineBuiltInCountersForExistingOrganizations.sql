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
Declare @organizations TABLE(rowNumber INT IDENTITY, TenantId UniqueIdentifier)

INSERT INTO @organizations
SELECT TenantId from Tenants;

DECLARE @count INT 
SELECT @count = COUNT(*) FROM @organizations;

DECLARE @index INT = 0;

DECLARE @TenantId UniqueIdentifier;

While (@index < @count)
BEGIN
SET @index = @index+1;
SELECT @tenantId = TenantId from @organizations where rowNumber = @index;

IF ((SELECT COUNT(*) FROM SequenceCounter where CounterName='StudentCounter' and TenantId=@TenantId)=0)
BEGIN
  INSERT INTO SequenceCounter(CounterType, TenantId,CounterValue,CounterName)
  Values('Tenant', @TenantId, 0, 'StudentCounter')

END

IF ((SELECT COUNT(*) FROM SequenceCounter where CounterName='RegistrationApplicationCounter' and TenantId=@TenantId)=0)
BEGIN
  INSERT INTO SequenceCounter(CounterType, TenantId,CounterValue,CounterName)
  Values('Tenant', @TenantId, 0, 'RegistrationApplicationCounter')

END

IF ((SELECT COUNT(*) FROM SequenceCounter where CounterName='ContactCounter' and TenantId=@TenantId)=0)
BEGIN
  INSERT INTO SequenceCounter(CounterType, TenantId,CounterValue,CounterName)
  Values('Tenant', @TenantId, 0, 'ContactCounter')

END

END