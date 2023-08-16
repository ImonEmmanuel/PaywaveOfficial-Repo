using MongoDB.Bson.Serialization.Attributes;
using PaywaveAPIData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PaywaveAPICore.Authentication
{
    public class UpdateDataModel
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string middleName { get; set; }
        public string bvn { get; set; }
        public Address address { get; set; }
        public NextOfKin next_of_kin { get; set; }

        public string phoneNumber { get; set; }
    }
}
