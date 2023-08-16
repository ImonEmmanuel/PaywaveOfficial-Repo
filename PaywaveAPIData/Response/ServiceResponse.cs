using PaywaveAPIData.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaywaveAPIData.Response
{
    public class ServiceResponse<T> : BaseResponse where T : class
    {
        public T data { get; set; }
        public string message { get; set; }
        public bool status { get; set; } = true;

        public ServiceResponse()
        {
            statusCode = base.statusCode;
        }
        public ServiceResponse(ResponseStatus status) : base(status)
        {
            statusCode = ResponseStatus.OK;
            
        }
    }
}
