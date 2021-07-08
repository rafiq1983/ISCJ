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

print ('Setting student numbers');

IF((SELECT COUNT(*) from sysobjects where name = 'SetStudentNumbersIfNecessary') >0)
BEGIN
   DROP PROCEDURE SetStudentNumbersIfNecessary
   
END
GO

CREATE Procedure SetStudentNumbersIfNecessary --creating SP as we don't have to worry variable conflicts.
AS
BEGIN

BEGIN TRANSACTION
DECLARE @students TABLE (rowNumber INT IDENTITY, StudentId UniqueIdentifier, TenantId UniqueIdentifier, StudentNumber int)

INSERT INTO @students(StudentId, TenantId, StudentNumber)
SELECT StudentId, TenantId, StudentNumber from Student;

DECLARE @count_SetStudentNumber INT ; 
SELECT @count_SetStudentNumber = COUNT(*) FROM @students;
DECLARE @index_SetStudentNumber INT =0;
DECLARE @tenantId UniqueIdentifier;
DECLARE @StudentNumber INT;
DECLARE @StudentId UniqueIdentifier;

WHILE(@index_SetStudentNumber < @count_SetStudentNumber)
BEGIN

SET @index_SetStudentNumber = @index_SetStudentNumber+1;

SELECT @StudentId = StudentId, @StudentNumber = StudentNumber, @tenantId = TenantId 
from @students where rowNumber = @index_SetStudentNumber;

if(@studentNumber IS NULL or @studentNumber = 0)
 BEGIN
  
   DECLARE @studentCounter INT
   SELECT @studentCounter = CounterValue from SequenceCounter where TenantId=@TenantId and CounterName = 'StudentCounter';
   SET @studentCounter = @studentCounter + 1;
   UPDATE SequenceCounter SET CounterValue = @studentCounter where TenantId=@TenantId and CounterName = 'StudentCounter';
   UPDATE Student SET StudentNumber = @studentCounter Where StudentId = @StudentId;

 END


END


 COMMIT TRANSACTION;
END

GO
EXEC SetStudentNumbersIfNecessary
GO
DROP Procedure SetStudentNumbersIfNecessary;
GO
print 'end of SetStudentIfNecessary';