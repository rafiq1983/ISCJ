CREATE TABLE [dbo].[Teachers] (
    [TeacherId]    UNIQUEIDENTIFIER NOT NULL,
    [ContactId]    UNIQUEIDENTIFIER NOT NULL,
    [TenantId]     UNIQUEIDENTIFIER NOT NULL,
    [CreateDate]   DATETIME         NOT NULL,
    [CreateUser]   VARCHAR (50)     NOT NULL,
    [ModifiedDate] DATETIME         NULL,
    [ModifiedUser] VARCHAR (50)     NULL,
    [RowVersion]   ROWVERSION       NOT NULL,
    CONSTRAINT [PK_Teacher] PRIMARY KEY CLUSTERED ([TeacherId] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Teacher]
    ON [dbo].[Teachers]([ContactId] ASC);

