using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication1.Refit
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticatedHttpClientHandler: HttpClientHandler
    {
        readonly Func<Task<string>> tokenFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenFactory"></param>
        public AuthenticatedHttpClientHandler(Func<Task<string>> tokenFactory)
        {
            this.tokenFactory = tokenFactory ?? throw new ArgumentNullException("getToken");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var auth = request.Headers.Authorization;
            if (auth == null)
            {
                var token = await tokenFactory().ConfigureAwait(false);
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", token);
            }

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
