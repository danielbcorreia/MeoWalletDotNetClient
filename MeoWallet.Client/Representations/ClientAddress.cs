using Newtonsoft.Json;

namespace MeoWallet
{
    public class ClientAddress
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        // TODO undocumented
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("postalcode")]
        public string PostalCode { get; set; }
    }
}