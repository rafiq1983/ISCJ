CREATE TABLE [dbo].[RegistrationApplication] (
    [ApplicationId]   UNIQUEIDENTIFIER NOT NULL,
    [ApplicationDate] DATETIME         NOT NULL,
    [ProgramId]       UNIQUEIDENTIFIER NOT NULL,
    [FatherContactId] UNIQUEIDENTIFIER NOT NULL,
    [MotherContactId] UNIQUEIDENTIFIER NOT NULL,
    [MembershipId]    UNIQUEIDENTIFIER NOT NULL,
    [CreateDate]      DATETIME         NOT NULL,
    [CreateUser]      VARCHAR (50)     NOT NULL,
    [ModifiedDate]    DATETIME         NULL,
    [ModifiedUser]    VARCHAR (50)     NULL,
    [TenantId] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [PK_RegistrationApplication] PRIMARY KEY CLUSTERED ([ApplicationId] ASC)
);

