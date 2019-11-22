CREATE TABLE [dbo].[MasjidMembership] (
    [MembershipId]   UNIQUEIDENTIFIER NOT NULL,
    [EffectiveDate]  DATETIME         NOT NULL,
    [ExpirationDate] DATETIME         NOT NULL,
    [ContactId]      UNIQUEIDENTIFIER NOT NULL,
    [CreateDate]     DATETIME         NOT NULL,
    [CreateUser]     VARCHAR (50)     NOT NULL,
    [ModifiedDate]   DATETIME         NULL,
    [ModifiedUser]   VARCHAR (50)     NULL, 
    [TenantId] UNIQUEIDENTIFIER NOT NULL
);

