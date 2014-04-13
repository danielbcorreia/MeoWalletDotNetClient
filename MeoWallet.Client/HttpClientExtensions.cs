using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MeoWallet
{
    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, string requestUri, T value, JsonSerializerSettings settings)
        {
            return PostAsJsonAsync(client, requestUri, value, settings, CancellationToken.None);
        }

        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, string requestUri, T value, JsonSerializerSettings settings, CancellationToken cancellationToken)
        {
            return client.PostAsync(requestUri, value, new JsonMediaTypeFormatter { SerializerSettings = settings }, cancellationToken);
        }
    }
}