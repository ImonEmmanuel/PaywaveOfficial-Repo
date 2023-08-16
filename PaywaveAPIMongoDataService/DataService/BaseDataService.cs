using Microsoft.Extensions.Options;
using PaywaveAPICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaywaveAPIData
    .DataService
{
    public abstract class BaseDataService
    {
        protected readonly AppSettings _appSettings;
        public BaseDataService(IOptions<AppSettings> appSettings)
        {
            _appSettings = new AppSettings();
        }
    }
}
