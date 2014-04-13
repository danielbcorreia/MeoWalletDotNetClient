using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MeoWallet
{
    public class MeoWalletHttpClientFactory
    {
        private readonly Uri _baseAddress;
        private readonly HttpMessageHandler _handler;

        public MeoWalletHttpClientFactory()
            : this(
                new Uri(ConfigurationManager.AppSettings["WalletBaseUri"]), 
                ConfigurationManager.AppSettings["MerchantApiKey"]
                )
        {
        }

        public MeoWalletHttpClientFactory(Uri baseAddress, string merchantApiKey)
        {
            var handlers = new[]
            {
                new MeoWalletAuthenticationHandler(merchantApiKey) 
            };

            var innerHandler = new HttpClientHandler
            {
                AllowAutoRedirect = true
            };

            _baseAddress = baseAddress;
            _handler = HttpClientFactory.CreatePipeline(innerHandler, handlers);
        }

        public HttpClient Create()
        {
            var client = new HttpClient(_handler, false)
            {
                BaseAddress = _baseAddress
            };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}