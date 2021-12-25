using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace QRMSWeb.Services
{
    public class AuthServiceHandler: DelegatingHandler
    {
        public AuthServiceHandler()
        { }
        
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return await base.SendAsync(request, cancellationToken);
        }
    }
}