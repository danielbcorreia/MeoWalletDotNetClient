using Newtonsoft.Json;

namespace MeoWallet
{
    public class RequiredFields
    {
        [JsonProperty("shipping")]
        public bool Shipping { get; set; }

        [JsonProperty("name")]
        public bool Name { get; set; }

        [JsonProperty("email")]
        public bool Email { get; set; }

        [JsonProperty("phone")]
        public bool Phone { get; set; }
    }
}