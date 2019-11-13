CREATE TABLE [dbo].[NotePointer] (
    [NoteId]        UNIQUEIDENTIFIER NOT NULL,
    [NoteAboutId]   UNIQUEIDENTIFIER NOT NULL,
    [NoteAboutType] VARCHAR (100)    NOT NULL,
    [TenantId]      UNIQUEIDENTIFIER NOT NULL,
    [CreateDate]    DATETIME         NOT NULL,
    [CreateUser]    VARCHAR (50)     NOT NULL,
    [ModifiedDate]  DATETIME         NULL,
    [ModifiedUser]  VARCHAR (50)     NULL,
    [RowVersion]    ROWVERSION       NOT NULL
);

