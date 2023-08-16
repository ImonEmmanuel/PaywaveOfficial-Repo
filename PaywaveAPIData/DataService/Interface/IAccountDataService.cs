using PaywaveAPIData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaywaveAPIData.DataService.Interface
{
    public interface IAccountDataService
    {
        Account GetAccountbyId(string id);
        Account GetbyClientId(string clientId);
        Account GetbyAccountNo(string accountNo);
        Task<bool> CreateAccount(Account account);
        bool UpdateAccount(string id, Account account);
        bool DeleteAccount(string id);
    }
}
