using Newtonsoft.Json;

namespace MeoWallet
{
    public class CreditCard
    {
        [JsonProperty("expired")]
        public bool Expired { get; set; }

        // card expiration date as MM/YYYY
        [JsonProperty("valdate")]
        public string ValidUntil { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("last4")]
        public string Last4 { get; set; }

        // UID representing the card in MEOWallet - for reference only
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}