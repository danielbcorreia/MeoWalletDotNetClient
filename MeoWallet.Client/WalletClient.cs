using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace MeoWallet
{
    public class WalletClient
    {
        private readonly MeoWalletHttpClientFactory _clientFactory;

        public WalletClient()
        {
            _clientFactory = new MeoWalletHttpClientFactory();
        }

        public WalletClient(MeoWalletHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        #region Checkout

        /// <summary>
        /// Starts a new checkout.
        /// If no callback parameters are given, we look for these appsettings: cancelWalletCallbackUrl, confirmWalletCallbackUrl
        /// </summary>
        public async Task<Checkout> StartCheckout(
            double amount, 
            PaymentItem[] paymentItems,
            ISO4217CurrencyCodes currency = ISO4217CurrencyCodes.EUR,
            ClientData clientData = null, 
            string cancelCallback = null,
            string confirmCallback = null,
            RequiredFields requiredFields = null,
            PaymentMethod[] paymentMethodExclusions = null)
        {
            using (var client = _clientFactory.Create())
            {
                var data = new Checkout
                {
                    Payment = new Operation
                    {
                        Amount = amount,
                        Currency = currency,
                        Items = paymentItems,
                        ClientData = clientData
                    },
                    RequiredFields = requiredFields,
                    Exclude = paymentMethodExclusions ?? new PaymentMethod[0],
                    UrlCancel = cancelCallback ?? ConfigurationManager.AppSettings["cancelWalletCallbackUrl"],
                    UrlConfirm = confirmCallback ?? ConfigurationManager.AppSettings["confirmWalletCallbackUrl"],
                };

                var response = await client.PostAsJsonAsync("checkout", data, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                })
                .ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    throw new MeoWalletApiException(await response.Content.ReadAsAsync<Error>().ConfigureAwait(false));
                }

                var result = await response.Content.ReadAsAsync<Checkout>().ConfigureAwait(false);

                return result;
            }
        }

        public async Task<Checkout> GetCheckout(Guid id)
        {
            using (var client = _clientFactory.Create())
            {
                // TODO bug in docs: "Content-Type: application/json"?? Change to Accept...
                var response = await client.GetAsync("checkout/" + id).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    throw new MeoWalletApiException(await response.Content.ReadAsAsync<Error>().ConfigureAwait(false));
                }

                var result = await response.Content.ReadAsAsync<Checkout>().ConfigureAwait(false);

                return result;
            }
        }

        public async Task<Checkout> DeleteCheckout(Guid id)
        {
            using (var client = _clientFactory.Create())
            {
                var response = await client.DeleteAsync("checkout/" + id).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    throw new MeoWalletApiException(await response.Content.ReadAsAsync<Error>().ConfigureAwait(false));
                }

                var result = await response.Content.ReadAsAsync<Checkout>().ConfigureAwait(false);

                return result;
            }
        }
        
        #endregion

        #region Operations

        public async Task<OperationList> ListOperations(int limit = 5, int offset = 0, DateTime? startDate = null, DateTime? endDate = null)
        {
            using (var client = _clientFactory.Create())
            {
                var queryParameters = new Dictionary<string, string>
                {
                    { "limit", limit.ToString(CultureInfo.InvariantCulture) },
                    { "offset", offset.ToString(CultureInfo.InvariantCulture) },
                    { "start-date", startDate.HasValue ? startDate.Value.ToString("yyyy-MM-dd") : null },
                    { "end-date", endDate.HasValue ? endDate.Value.ToString("yyyy-MM-dd") : null },
                };

                var response = await client.GetAsync("operations?" + BuildQueryString(queryParameters)).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    throw new MeoWalletApiException(await response.Content.ReadAsAsync<Error>().ConfigureAwait(false));
                }

                var result = await response.Content.ReadAsAsync<OperationList>().ConfigureAwait(false);

                return result;
            }
        }

        public async Task<Operation> GetOperation(Guid id)
        {
            using (var client = _clientFactory.Create())
            {
                var response = await client.GetAsync("operations/" + id).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    throw new MeoWalletApiException(await response.Content.ReadAsAsync<Error>().ConfigureAwait(false));
                }

                var result = await response.Content.ReadAsAsync<Operation>().ConfigureAwait(false);

                return result;
            }
        }

        public async Task<Operation> RefundOperation(Guid id, RefundRequest refundRequest = null)
        {
            using (var client = _clientFactory.Create())
            {
                var response = await client.PostAsJsonAsync(
                    string.Format("operations/{0}/refund", id), 
                    refundRequest ?? new RefundRequest(), 
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    })
                    .ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    throw new MeoWalletApiException(await response.Content.ReadAsAsync<Error>().ConfigureAwait(false));
                }

                var result = await response.Content.ReadAsAsync<Operation>().ConfigureAwait(false);

                return result;
            }
        }

        #endregion

        #region Transfers

        // TODO undocumented
        public async Task<Operation> Transfer(string toEmail, double amount)
        {
            // only for authorized users

            using (var client = _clientFactory.Create())
            {
                var response = await client.PostAsJsonAsync(string.Format(
                    "users/{0}/transfer", WebUtility.UrlEncode(toEmail)), 
                    new Operation
                    {
                        Amount = amount,
                        Method = PaymentMethod.Wallet
                    }, 
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    })
                    .ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    throw new MeoWalletApiException(await response.Content.ReadAsAsync<Error>().ConfigureAwait(false));
                }

                var result = await response.Content.ReadAsAsync<Operation>().ConfigureAwait(false);

                return result;
            }
        }

        #endregion

        #region Callbacks

        public async Task<Callback> ReceiveCallback(string payload)
        {
            // TODO basic auth on the callback request??

            using (var client = _clientFactory.Create())
            {
                var response = await client.PostAsync("callback/verify", new StringContent(payload, Encoding.UTF8, "application/json")).ConfigureAwait(false);
                var result = await response.Content.ReadAsAsync<bool>().ConfigureAwait(false);

                // If the callback is not valid verify answers 400 Bad Request and a single field "false" on the body
                if (response.StatusCode == HttpStatusCode.BadRequest && !result)
                {
                    throw new InvalidCallbackException();
                }

                // ... it's a valid callback by responding with status 200 OK and a single field "true" on the body
                if (!response.IsSuccessStatusCode)
                {
                    throw new MeoWalletApiException(await response.Content.ReadAsAsync<Error>().ConfigureAwait(false));
                }

                var callback = await response.Content.ReadAsAsync<Callback>().ConfigureAwait(false);

                return callback;
            }
        }

        #endregion

        #region Utilities

        private string BuildQueryString(Dictionary<string, string> queryParameters)
        {
            var qs = new StringBuilder();

            foreach (var param in queryParameters)
            {
                if (qs.Length > 0)
                {
                    qs.Append('&');
                }

                qs.Append(WebUtility.UrlEncode(param.Key));
                qs.Append('=');
                qs.Append(WebUtility.UrlEncode(param.Value));
            }

            return qs.ToString();
        }

        #endregion
    }
}
