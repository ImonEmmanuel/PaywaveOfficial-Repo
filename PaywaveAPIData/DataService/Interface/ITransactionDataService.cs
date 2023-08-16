using PaywaveAPIData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaywaveAPIData.DataService.Interface
{
    public interface ITransactionDataService
    {
        Transaction GetTransactionbyId(string id);
        Transaction GetbyTransactionRef(string transactionRefrenece);
        IEnumerable<Transaction> GetbyAccountNo(string accountNo);
        Task<bool> InsertTransaction(Transaction transact);
    }
}
