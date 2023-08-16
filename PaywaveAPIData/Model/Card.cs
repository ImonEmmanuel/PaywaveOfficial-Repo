using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaywaveAPIData.Model
{
    public class Card
    {
        [BsonId]
        public string ID;
        public string AccountNo { get; set; }   = String.Empty;
        public DateTime ExpirationDate { get; set; }
        public string CardNo { get; set; } = String.Empty;
        [JsonIgnore]
        public CardType cardType { get; set; } = CardType.PaywaveCoreStaff;
        public string AccountID { get; set; }

    }

    public enum CardType
    {
        PaywaveCoreStaff,
        UnibenStaff,
        UnibenStudentCard
    }
}
