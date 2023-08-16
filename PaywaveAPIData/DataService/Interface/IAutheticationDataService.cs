using PaywaveAPIData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaywaveAPIData.DataService.Interface
{
    public interface IAutheticationDataService
    {
        Client GetClientById(string clientId);
        List<Client> GetClientByUserName(string userName);
        bool UpdateClient(Client client);
        bool DeleteClient(string clientId);
        Task<bool> InsertClient(Client client);
        Client GetClientByEmail(string emailAddress);
    }
}
