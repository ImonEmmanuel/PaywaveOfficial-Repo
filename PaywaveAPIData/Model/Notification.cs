using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PaywaveAPIData.Model
{
    public class Notification
    {
        [BsonId]
        public string ID { get; set; }
        [JsonIgnore]
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public object data { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public NotificationChannel notificationChannel { get; set; } = NotificationChannel.InApp;

        [JsonConverter(typeof(StringEnumConverter))]
        public NotificationType notificationType { get; set; } = NotificationType.AccountActivity;

        public Dictionary<string, string> Details { get; set; }

        public bool MarkedAsRead { get; set; } = false;
        public DateTime DateCreated { get; set; }
    }

    // Enum to represent different notification channels
    public enum NotificationChannel
    {
        Email,
        SMS,
        InApp
    }

    // Enum to represent different types of notifications
    public enum NotificationType
    {
        AccountActivity,
        Transaction,
        ImportantUpdate
    }
}
