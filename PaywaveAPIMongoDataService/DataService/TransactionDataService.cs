using Microsoft.Extensions.Options;
using MongoDB.Driver;
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
    public class TransactionDataService : BaseDataService, ITransactionDataService
    {
        string databaseName = DataServiceConstant.databaseCollection;
        string tableName = DataServiceConstant.transactionCollection;
        private readonly string _connectionString;

        public TransactionDataService(IOptions<AppSettings> appSettings) : base(appSettings)
        {
            _connectionString = appSettings.Value.MongoDB_ConnectionString;
        }

        public Transaction GetTransactionbyId(string id)
        {
            MongoDataBase database = new MongoDataBase(databaseName, _connectionString);
            return database.LoadRecordById<Transaction>(tableName, id);
        }

        public Transaction GetbyTransactionRef(string transactionRefrenece)
        {
            MongoDataBase database = new MongoDataBase(databaseName, _connectionString);
            return database.LoadRecordById<Transaction>(tableName, transactionRefrenece, idField: "TransactionRefrence");
        }

        public IEnumerable<Transaction> GetbyAccountNo(string accountNo)
        {
            MongoDataBase database = new MongoDataBase(databaseName, _connectionString);
            var filter = Builders<Transaction>.Filter.Where(x => x.SourceAccountNumber == accountNo);
            return database.LoadRecordsByFilter<Transaction>(tableName, filter);
        }

        public async Task<bool> InsertTransaction(Transaction transact)
        {
            MongoDataBase database = new MongoDataBase(databaseName, _connectionString);
            return await database.InsertRecord(tableName, transact);
        }
    }
}
