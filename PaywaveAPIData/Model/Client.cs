using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PaywaveAPIData.Model
{
    public class Client
    {
        [BsonId]
        public string ID;
        public string userName;
        public string firstName;
        public string lastName;
        public string middleName;
        public string email;
        public string bvn { get; set; }
        public Address address { get; set; }
        public NextOfKin next_of_kin { get; set; }
        [JsonIgnore]
        public string password;
        public bool isVerified { get; set; }
        public bool hasPin { get; set; }
        public string deviceToken { get; set; }
        public string phoneNumber { get; set; }
    }
    public class Address
    {
        public string street { get; set; }
        public string city { get; set; }
        public string local_government { get; set; }
        public string country { get; set; }
    }

    public class NextOfKin
    {
        public string name { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string relationship { get; set; }
        public Address address { get; set; }
    }
}
