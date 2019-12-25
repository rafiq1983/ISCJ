using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace MA.Common
{
    public class AddUserNotificationInput
    {
        public Guid UserId { get; set; }
        public string NotificationType { get; set; }
        public string NotificationData { get; set; }
    }
}
