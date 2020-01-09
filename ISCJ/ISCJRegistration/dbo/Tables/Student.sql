CREATE TABLE [dbo].[Student] (
    [StudentId]          UNIQUEIDENTIFIER NOT NULL,
    [ContactId]          UNIQUEIDENTIFIER NOT NULL,
    [FatherContactId]    UNIQUEIDENTIFIER NULL,
    [MotherContactId]    UNIQUEIDENTIFIER NULL,
    [EmergencyContactId] UNIQUEIDENTIFIER NULL,
    [CreateDate]         DATETIME         NOT NULL,
    [CreateUser]         VARCHAR(50) NOT NULL,
    [ModifiedDate]       DATETIME         NULL,
    [ModifiedUser]       UNIQUEIDENTIFIER NULL,
    [RowVersion]         ROWVERSION       NOT NULL, 
    [TenantId] UNIQUEIDENTIFIER NOT NULL, 
    [StudentNumber] INT NULL  
);


GO

