CREATE TABLE [dbo].[UserPersona] (
    [UserId]       UNIQUEIDENTIFIER NOT NULL,
    [PersonaId]    UNIQUEIDENTIFIER NOT NULL,
    [CreateDate]   DATETIME         NOT NULL,
    [CreateUser]   VARCHAR (50)     NOT NULL,
    [ModifiedDate] DATETIME         NULL,
    [ModifiedUser] VARCHAR (50)     NULL,
    [TenantId] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [PK_UserPersona] PRIMARY KEY CLUSTERED ([UserId] ASC, [PersonaId] ASC)
);

