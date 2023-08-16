using PaywaveAPIData.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaywaveAPIData.Response
{
    public class BaseResponse
    {
        public ResponseStatus statusCode{ get; set; }

        public BaseResponse()
        {
            statusCode = ResponseStatus.OK;
        }
        public BaseResponse(ResponseStatus status)
        {
            statusCode = status;
        }
    }
}
