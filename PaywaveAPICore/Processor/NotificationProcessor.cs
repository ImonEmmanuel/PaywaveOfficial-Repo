using FirebaseAdmin.Messaging;
using PaywaveAPIData.DataService.Interface;
using PaywaveAPIData.Enum;
using PaywaveAPIData.Model;
using PaywaveAPIData.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notification = PaywaveAPIData.Model.Notification;

namespace PaywaveAPICore.Processor
{
    public class NotificationProcessor
    {
        private readonly INotificationDataService _notificationDataService;
        public NotificationProcessor(INotificationDataService notificationDataService)
        {
            _notificationDataService = notificationDataService;
        }

        public Task SendNotificationToAllUsers(Notification notify, string message)
        {
            throw new NotImplementedException();
        }
        public Task SendEmailNotification(Notification notify, string message)
        {
            throw new NotImplementedException();
        }
        public Task SendSMSNotification(Notification notify, string message)
        {
            throw new NotImplementedException();
        }
        public Task SendInAppNotification(Notification notify, string message)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<IEnumerable<Notification>> GetNotifications(string userId)
        {
            ServiceResponse<IEnumerable<Notification>> data = new ServiceResponse<IEnumerable<Notification>>();

            if (string.IsNullOrEmpty(userId))
            {
                data.statusCode = ResponseStatus.NOT_FOUND;
                data.message = nameof(userId).ToString() + "userId is required";
                throw new Exception(data.ToString());
            }
            data.data= _notificationDataService.GetNotifications(userId);
            data.statusCode = ResponseStatus.OK;
            return data;
        }

        public async Task<string> SendFireBaseNotification(string deviceToken, Notification notify)
        {
            // Initialize the FirebaseMessaging client
            var messaging = FirebaseMessaging.DefaultInstance;

            var dictionary = new Dictionary<string, string>();

            // Get the type of the model object
            var type = notify.GetType();
            foreach (var property in type.GetProperties())
            {
                var value = property.GetValue(notify);
                dictionary.Add(property.Name, value.ToString());
            }
            // Create a notification message
            var message = new Message()
            {
                Notification = new FirebaseAdmin.Messaging.Notification()
                {
                    Title = CreateMessage(notify),
                    Body = notify.Message,
                },
                Token = deviceToken,
                Data = dictionary
            };
            // Send the notification message
            string response = await messaging.SendAsync(message);
            return response;
        }

        public ServiceResponse<string> UpdateReadStatus(string userId, string notificationId, bool isRead)
        {
            ServiceResponse<string> resp = new ServiceResponse<string>();

            if (string.IsNullOrEmpty(userId))
            {
                resp.message = "userId is required";
                resp.statusCode= ResponseStatus.UNAUTHORIZED;
                return resp;
            }
            if (string.IsNullOrEmpty(notificationId))
            {
                resp.message = "notificationId is required";
                resp.statusCode = ResponseStatus.UNAUTHORIZED;
                return resp;
            }

            Notification notification = _notificationDataService.GetNotificationById(notificationId, userId);

            if (notification == null)
            {
                resp.message = "Cannot find notification";
                resp.statusCode = ResponseStatus.NOT_FOUND;
                return resp;
            }

            notification.MarkedAsRead = isRead;
            if (!_notificationDataService.UpdateNotification(notification))
            {
                resp.message = "Notification Update Error";
                resp.statusCode = ResponseStatus.SERVER_ERROR;
                return resp;
            };
            resp.message = "Success";
            resp.data = "Notification successfully updated";
            return resp;
        }

        public ServiceResponse<string> DeleteNotification(string notificationId, string clientId)
        {
            if (string.IsNullOrEmpty(clientId))
                throw new ArgumentNullException(nameof(clientId), "clientId is required");

            if (string.IsNullOrEmpty(notificationId))
                throw new ArgumentNullException(nameof(notificationId), "notificationId is required");

            if(_notificationDataService.DeleteNotification(notificationId, clientId))
            {
                return new()
                {
                    data = "Data Deleted Successfully",
                    message = "success",
                    statusCode = ResponseStatus.ACCEPTED
                };
            };
            return new()
            {
                data = "Failed",
                message = "Process not complete Try Again",
                statusCode = ResponseStatus.UNPROCESSABLE,
            };
        }

        public string CreateMessage(Notification notify)
        {
            string Title = "";
            if (notify.notificationType.ToString() == "AccountActivity")
                Title = $"{notify.notificationType} AccountActivity Notification";
            else if (notify.notificationType.ToString() == "ImportantUpdate")
                Title = $"{notify.notificationType} App Update";
            else
                Title = $"{notify.notificationType} Transaction Alert";
            return Title;
        }
    }

}
