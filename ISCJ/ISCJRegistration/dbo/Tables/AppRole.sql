CREATE TABLE [dbo].[AppRole] (
    [RoleCd]          VARCHAR (50)  NOT NULL,
    [RoleDescription] VARCHAR (100) NOT NULL,
    [CreateDate]      DATETIME      NOT NULL,
    [CreateUser]      VARCHAR (50)  NOT NULL,
    [ModifiedDate]    DATETIME      NULL,
    [ModifiedUser]    VARCHAR (50)  NULL,
    [RowVersion]      ROWVERSION    NOT NULL,
    CONSTRAINT [PK_AppRole_1] PRIMARY KEY CLUSTERED ([RoleCd] ASC)
);

