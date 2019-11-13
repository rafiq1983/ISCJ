CREATE TABLE [dbo].[Programs] (
    [ProgramId]          UNIQUEIDENTIFIER NOT NULL,
    [ProgramName]        VARCHAR (100)    NOT NULL,
    [ProgramDescription] NVARCHAR (500)   NULL,
    [CreateDate]         DATETIME         NOT NULL,
    [CreateUser]         VARCHAR (50)     NOT NULL,
    [ModifiedDate]       DATETIME         NULL,
    [ModifiedUser]       VARCHAR (50)     NULL,
    CONSTRAINT [PK_Programs] PRIMARY KEY CLUSTERED ([ProgramId] ASC)
);

