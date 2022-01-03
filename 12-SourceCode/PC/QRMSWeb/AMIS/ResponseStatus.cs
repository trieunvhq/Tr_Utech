using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMIS
{
    public class ResponseStatus
    {
        public const int RequestSuccessed = 200;
        public const int RequestUnauthorized = 401;
        public const int RequestForbidden = 403;
        public const int RequestNotFound = 404;
        public const int RequestServerError = 500;
    }
}
