using PaywaveAPIData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaywaveAPIData.DataService.Interface
{
    public interface ITokenDataService
    {
        Task<RefreshToken> GetTokenbyClientId(string clientId);
        Task<RefreshToken> GetToken(string refreshToken);
        Task<bool> AddToken(RefreshToken token);
        bool RemoveToken(string tokenId);
        bool UpdateToken(RefreshToken token, string tokenId);
    }
}
