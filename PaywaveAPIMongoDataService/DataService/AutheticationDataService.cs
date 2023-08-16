using Microsoft.Extensions.Options;
using PaywaveAPICore;
using PaywaveAPICore.Authentication;
using PaywaveAPICore.Constant;
using PaywaveAPIData.DataService;
using PaywaveAPIData.DataService.Interface;
using PaywaveAPIData.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PaywaveAPIMongoDataService.DataService
{
    public class AutheticationDataService : BaseDataService, IAutheticationDataService
    {
        string databaseName = DataServiceConstant.databaseCollection;
        string tableName = DataServiceConstant.authCollection;
        private readonly string _connectionString;

        public AutheticationDataService(IOptions<AppSettings> appSettings) : base(appSettings)
        {
            _connectionString = appSettings.Value.MongoDB_ConnectionString;
        }

        public Client GetClientById(string clientId)
        {
            MongoDataBase database = new MongoDataBase(databaseName, _connectionString);
            return database.LoadRecordById<Client>(tableName, clientId);
        }

        public bool DeleteClient(string clientId)
        {
            MongoDataBase database = new MongoDataBase(databaseName, _connectionString);
            return database.DeleteRecord<Client>(tableName, clientId);
        }

        public async Task<bool> InsertClient(Client client)
        {
            MongoDataBase database = new MongoDataBase(databaseName, _connectionString);   
            return await database.InsertRecord<Client>(tableName, client);
        }

        public bool UpdateClient(Client client)
        {
            MongoDataBase database = new MongoDataBase(databaseName, _connectionString);
            return database.UpdateRecord<Client>(tableName, client, client.ID);
        }

        public Client GetClientByEmail(string emailAddress)
        {
            MongoDataBase database = new MongoDataBase(databaseName, _connectionString);
            return database.LoadRecordById<Client>(tableName, emailAddress, "email");
        }

        public List<Client> GetClientByUserName(string userName)
        {
            MongoDataBase database = new MongoDataBase(databaseName, _connectionString);
            return database.LoadRecordsByFilter<Client>(tableName, userName);
        }
        //public string DeleteAllClient()
        //{
        //    MongoDataBase database = new MongoDataBase(databaseName, _connectionString);
        //    return database.LoadAllRecord<Client>(tableName).Result;
        //}

    }
}
