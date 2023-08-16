using Microsoft.Extensions.Options;
using PaywaveAPICore.Constant;
using PaywaveAPICore;
using PaywaveAPIData.DataService.Interface;
using PaywaveAPIData.DataService;
using PaywaveAPIData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaywaveAPIMongoDataService.DataService
{
    public class TokenDataService : BaseDataService, ITokenDataService
    {
        string databaseName = DataServiceConstant.databaseCollection;
        string tableName = DataServiceConstant.tokenCollection;
        private readonly string _connectionString;

        public TokenDataService(IOptions<AppSettings> appSettings) : base(appSettings)
        {
            _connectionString = appSettings.Value.MongoDB_ConnectionString;
        }

        public Client GetClientById(string clientId)
        {
            MongoDataBase database = new MongoDataBase(databaseName, _connectionString);
            return database.LoadRecordById<Client>(tableName, clientId);
        }

        public async Task<bool> AddToken(RefreshToken token)
        {
            MongoDataBase database = new MongoDataBase(databaseName, _connectionString);
            return await database.InsertRecord(tableName, token);
        }

        public bool RemoveToken(string tokenId)
        {
            MongoDataBase database = new MongoDataBase(databaseName, _connectionString);
            return database.DeleteRecord<RefreshToken>(tableName, tokenId);
        }

        public bool UpdateToken(RefreshToken token, string tokenId)
        {
            MongoDataBase database = new MongoDataBase(databaseName, _connectionString);
            return database.UpdateRecord<RefreshToken>(tableName, token, tokenId);
        }

        public async Task<RefreshToken> GetTokenbyClientId(string clientId)
        {
            MongoDataBase database = new MongoDataBase(databaseName, _connectionString);
            var data = await database.GetAllRecord<RefreshToken>(tableName);
            if (data is not null)
                return data.Where(x => x.ClientId == clientId && x.revokedToken == false).FirstOrDefault();
            return null;

        }

        public async Task<RefreshToken> GetToken(string refreshToken)
        {
            //Fix this query method it is a poor design
            MongoDataBase database = new MongoDataBase(databaseName, _connectionString);
            return database.LoadRecordById<RefreshToken>(tableName, refreshToken, idField: "RefreshTokenAccess");
            //var data = await database.GetAllRecord<RefreshToken>(tableName);
            //if (data is not null)
            //    return data.Where(x => x.RefreshTokenAccess == refreshToken).FirstOrDefault();
            //return null;
        }
    }
}
