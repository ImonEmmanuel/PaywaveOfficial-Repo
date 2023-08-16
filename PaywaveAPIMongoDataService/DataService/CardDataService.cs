using Microsoft.Extensions.Options;
using PaywaveAPICore.Constant;
using PaywaveAPICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaywaveAPIData.DataService;
using PaywaveAPIData.Model;
using PaywaveAPIData.DataService.Interface;

namespace PaywaveAPIMongoDataService.DataService
{
    public class CardDataService : BaseDataService , ICardDataService
    {
        string databaseName = DataServiceConstant.databaseCollection;
        string tableName = DataServiceConstant.cardCollection;
        private readonly string _connectionString;

        public CardDataService(IOptions<AppSettings> appSettings) : base(appSettings)
        {
            _connectionString = appSettings.Value.MongoDB_ConnectionString;
        }

        public Card GetCardbyId(string id)
        {
            MongoDataBase database = new MongoDataBase(databaseName, _connectionString);
            return database.LoadRecordById<Card>(tableName, id);
        }

        public Card GetCardbyAccountNo(string accountNo)
        {
            MongoDataBase database = new MongoDataBase(databaseName, _connectionString);
            return database.LoadRecordById<Card>(tableName, accountNo, idField : "AccountNo");
        }

        public Card GetCardbyCardNumber(string cardNumber)
        {
            MongoDataBase database = new MongoDataBase(databaseName, _connectionString);
            return database.LoadRecordById<Card>(tableName, cardNumber, idField: "CardNo");
        }

        public async Task<bool> CreateCard(Card card)
        {
            MongoDataBase database = new MongoDataBase(databaseName, _connectionString);
            return await database.InsertRecord<Card>(tableName, card);
        }

        public bool DeleteCard(string id)
        {
            MongoDataBase database = new MongoDataBase(databaseName, _connectionString);
            return database.DeleteRecord<Card>(tableName, id);
        }
    }
}
