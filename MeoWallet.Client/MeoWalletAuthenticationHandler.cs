using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace MeoWallet
{
    public class MeoWalletAuthenticationHandler : DelegatingHandler
    {
        private const string Scheme = "WalletPT";

        private readonly string _merchantApiKey;

        public MeoWalletAuthenticationHandler(string merchantApiKey)
        {
            _merchantApiKey = merchantApiKey;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue(Scheme, _merchantApiKey);
            return base.SendAsync(request, cancellationToken);
        }
    }
}