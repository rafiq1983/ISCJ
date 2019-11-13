CREATE TABLE [dbo].[Persona] (
    [PersonaId]   UNIQUEIDENTIFIER NOT NULL,
    [PersonaCd]   VARCHAR (20)     NOT NULL,
    [Description] VARCHAR (500)    NOT NULL,
    CONSTRAINT [PK_Persona] PRIMARY KEY CLUSTERED ([PersonaId] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Persona_PersonaCd]
    ON [dbo].[Persona]([PersonaCd] ASC);

