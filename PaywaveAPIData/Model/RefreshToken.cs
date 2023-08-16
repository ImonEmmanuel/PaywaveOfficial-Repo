using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaywaveAPIData.Model
{
    public class RefreshToken
    {
        [BsonId]
        public string ID;

        public string ClientId { get; set; }

        public string ClientEmail { get; set; } = string.Empty;

        public string RefreshTokenAccess { get; set; } = string.Empty;
        public bool revokedToken { get; set; } = false; //if a token has been revoked this should be true

        public DateTime ExpireAt { get; set; }

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
