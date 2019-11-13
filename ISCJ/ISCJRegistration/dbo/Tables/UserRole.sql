CREATE TABLE [dbo].[UserRole] (
    [RoleCd]       UNIQUEIDENTIFIER NOT NULL,
    [UserId]       UNIQUEIDENTIFIER NOT NULL,
    [CreateDate]   DATETIME         NOT NULL,
    [CreateUser]   UNIQUEIDENTIFIER NOT NULL,
    [ModifiedDate] DATETIME         NULL,
    [ModifiedUser] UNIQUEIDENTIFIER NULL,
    [RowVersion]   ROWVERSION       NOT NULL,
    CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED ([RoleCd] ASC, [UserId] ASC)
);

