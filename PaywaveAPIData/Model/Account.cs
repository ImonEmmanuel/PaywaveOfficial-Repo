using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PaywaveAPIData.Model
{
    public class Account
    {
        [BsonId]
        public string ID;
        [JsonIgnore]
        public string ClientID { get; set; } // This is the Id Key of the account
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public decimal? PendingBalance { get; set; } // for Settlement
        public decimal? AvailableBalance { get; set; }//Balance The Customer have that can be withdarwal at that time
        public DateTime? LastRefreshed { get; set; }
        public string TransactionPin { get; set; }
    }
}
