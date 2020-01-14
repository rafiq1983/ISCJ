CREATE TABLE [dbo].[SystemSetttings]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [AllowCreateOrganization] TINYINT NOT NULL, 
    [MaxOrganizationsAllowed] INT NOT NULL, 
    [SetupMode] VARCHAR(50) NOT NULL, 
    [CanUserSignupOnTheirOwn] TINYINT NOT NULL, 
    [SystemLocale] CHAR(5) NOT NULL DEFAULT 'en-Us', 
    [AllowedSimultaneousLoginToOrganization] INT NOT NULL DEFAULT 0, 
    [License] VARCHAR(MAX) NOT NULL, 
    [DeploymentReferenceId] UNIQUEIDENTIFIER NOT NULL, 
    [VerificationPublicKey] VARCHAR(500) NOT NULL
)
