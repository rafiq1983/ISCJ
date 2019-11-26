CREATE TABLE [dbo].[UserNotification]
(
	[NotificationId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [NotificationType] VARCHAR(20) NOT NULL, 
    [NotificationData] VARCHAR(500) NOT NULL, 
    [CreateDate] DATETIME NOT NULL, 
    [CreateUser] VARCHAR(50) NOT NULL, 
    [ModifiedDate] DATETIME NULL, 
    [ModifiedUser] VARCHAR(50) NULL
)
