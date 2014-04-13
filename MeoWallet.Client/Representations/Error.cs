using Newtonsoft.Json;

namespace MeoWallet
{
    // https://developers.wallet.pt/procheckout/errorhandling

    public class Error
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("tid")]
        public string Tid { get; set; }
    }
}
