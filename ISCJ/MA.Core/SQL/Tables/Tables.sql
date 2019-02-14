/****** Object:  Table [dbo].[Registration]    Script Date: 2/14/2019 5:35:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Registration](
	[RegistrationId] [uniqueidentifier] NOT NULL,
	[FatherId] [uniqueidentifier] NOT NULL,
	[MotherId] [uniqueidentifier] NOT NULL,
	[StudentId] [uniqueidentifier] NOT NULL,
	[IslamicSchoolGradeId] [varchar](10) NULL,
	[PublicSchoolGradeId] [varchar](10) NULL,
	[ProgramId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Registration] PRIMARY KEY CLUSTERED 
(
	[RegistrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


