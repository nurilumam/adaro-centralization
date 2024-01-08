using System;
using Abp.Notifications;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.Notifications.Dto
{
    public class GetUserNotificationsInput : PagedInputDto
    {
        public UserNotificationState? State { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}