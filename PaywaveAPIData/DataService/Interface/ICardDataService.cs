using PaywaveAPIData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaywaveAPIData.DataService.Interface
{
    public interface ICardDataService 
    {
        Card GetCardbyId(string id);
        Card GetCardbyAccountNo(string accountNo);
        Card GetCardbyCardNumber(string cardNumber);
        Task<bool> CreateCard(Card card);
        bool DeleteCard(string id);
    }
}
