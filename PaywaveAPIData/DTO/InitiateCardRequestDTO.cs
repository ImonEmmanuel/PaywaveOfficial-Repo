using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaywaveAPIData.DTO
{
    public class InitiateCardRequestDTO
    {
        public string CardNumber { get; set; }
        public int Amount {get; set;}
        public string TransactionPin { get; set; } //to authorize transaction
        public string SecurityNumber { get; set; } //cvv
        public string ExpirationDate { get; set; } //mm/yy
        public string Narration { get;set; }
    }
}
