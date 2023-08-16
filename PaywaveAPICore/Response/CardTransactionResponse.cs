using PaywaveAPIData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaywaveAPICore.Response
{
    public class CardTransactionResponse
    {
        public string TransactionRefrence { get; set; }
        public string SourceAccountNumber { get; set; }
        public string SourceAccountName { get; set; }
        public decimal Amount { get; set; }
        public string SourceAccountClientId { get; set; }
        public string Narration { get; set; }
        public string BeneficiaryName { get; set; }
        public string BeneficiaryAccountNumber { get; set; }
        public string TransactionReference { get; set; }
    }
}
