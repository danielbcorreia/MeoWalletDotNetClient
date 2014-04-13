using Newtonsoft.Json;

namespace MeoWallet
{
    public class Merchant
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }
    }
}