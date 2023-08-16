using Microsoft.Extensions.Options;
using PaywaveAPICore;
using PaywaveAPICore.Constant;
using PaywaveAPIData.DataService;
using PaywaveAPIData.DataService.Interface;
using PaywaveAPIData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaywaveAPIMongoDataService.DataService
{
    public class AccountDataService : BaseDataService, IAccountDataService
    {
        string databaseName = DataServiceConstant.databaseCollection;
        string tableName = DataServiceConstant.accountCollection;
        private readonly string _connectionString;

        public AccountDataService(IOptions<AppSettings> appSettings) : base(appSettings)
        {
            _connectionString = appSettings.Value.MongoDB_ConnectionString;
        }

        public Account GetAccountbyId(string id)
        {
            MongoDataBase database = new MongoDataBase(databaseName, _connectionString);
            return database.LoadRecordById<Account>(tableName, id);
        }

        public Account GetbyAccountNo(string accountNo)
        {
            MongoDataBase database = new MongoDataBase(databaseName, _connectionString);
            return database.LoadRecordById<Account>(tableName, accountNo, idField: "AccountNumber");
        }

        public async Task<bool> CreateAccount(Account account)
        {
            MongoDataBase database = new MongoDataBase(databaseName, _connectionString);
            return await database.InsertRecord(tableName, account);
        }

        public bool UpdateAccount(string id, Account account)
        {
            MongoDataBase database = new MongoDataBase(databaseName, _connectionString);
            return database.UpdateRecord<Account>(tableName, account, id);
        }

        public bool DeleteAccount(string id)
        {
            MongoDataBase database = new MongoDataBase(databaseName, _connectionString);
            return database.DeleteRecord<Account>(tableName, id);
        }

        public Account GetbyClientId(string clientId)
        {
            MongoDataBase database = new MongoDataBase(databaseName, _connectionString);
            return database.LoadRecordById<Account>(tableName, clientId, idField: "ClientID");
        }

    }
}
