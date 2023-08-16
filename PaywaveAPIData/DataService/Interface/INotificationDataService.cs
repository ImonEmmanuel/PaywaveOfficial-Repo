using PaywaveAPIData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaywaveAPIData.DataService.Interface
{
    public interface INotificationDataService
    {
        IEnumerable<Notification> GetNotifications(string userId);
        Notification GetNotificationById(string id, string userId);
        Task<bool> InsertNotification(Notification notification);
        bool UpdateNotification(Notification notification);
        bool DeleteNotification(string id, string userId);
    }
}
