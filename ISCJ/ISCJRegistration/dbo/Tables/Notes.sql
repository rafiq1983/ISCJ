CREATE TABLE [dbo].[Notes] (
    [NoteId]       UNIQUEIDENTIFIER NOT NULL,
    [TenantId]     UNIQUEIDENTIFIER NOT NULL,
    [Note]         VARCHAR (3000)   NOT NULL,
    [CreateDate]   DATETIME         NOT NULL,
    [CreateUser]   VARCHAR (50)     NOT NULL,
    [ModifiedDate] DATETIME         NULL,
    [ModifiedUser] VARCHAR (50)     NULL,
    [RowVersion]   ROWVERSION       NOT NULL,
    CONSTRAINT [PK_Notes] PRIMARY KEY CLUSTERED ([NoteId] ASC)
);

