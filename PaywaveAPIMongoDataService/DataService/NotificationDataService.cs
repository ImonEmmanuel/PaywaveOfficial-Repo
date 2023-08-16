using Microsoft.Extensions.Options;
using PaywaveAPICore.Constant;
using PaywaveAPICore;
using PaywaveAPIData.DataService;
using PaywaveAPIData.DataService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaywaveAPIData.Model;

namespace PaywaveAPIMongoDataService.DataService
{
    public class NotificationDataService : BaseDataService, INotificationDataService
    {
        string databaseName = DataServiceConstant.databaseCollection;
        string tableName = DataServiceConstant.notifyCollection;
        private readonly string _connectionString;

        public NotificationDataService(IOptions<AppSettings> appSettings) : base(appSettings)
        {
            _connectionString = appSettings.Value.MongoDB_ConnectionString;
        }

        public IEnumerable<Notification> GetNotifications(string userId)
        {
            MongoDataBase database = new MongoDataBase(databaseName, _connectionString);
            return database.LoadRecordsByFilter<Notification>(tableName, userId);
        }

        public Notification GetNotificationById(string id, string userId)
        {
            var data = GetNotifications(userId);
            return data.Where(x => x.ID == id).FirstOrDefault();
        }

        public async Task<bool> InsertNotification(Notification notification)
        {
            MongoDataBase database = new MongoDataBase(databaseName, _connectionString);
            return await database.InsertRecord<Notification>(tableName, notification);
        }

        public bool UpdateNotification(Notification notification)
        {
            MongoDataBase database = new MongoDataBase(databaseName, _connectionString);
            return database.UpdateRecord<Notification>(tableName, notification,id: notification.ID);    

        }

        public bool DeleteNotification(string id, string userId)
        {
            MongoDataBase database = new MongoDataBase(databaseName, _connectionString);
            Notification data = GetNotificationById(id, userId);
            return database.DeleteRecord<Notification>(tableName, id: data.ID);
        }
    }
}
