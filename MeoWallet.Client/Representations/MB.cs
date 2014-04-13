using Newtonsoft.Json;

namespace MeoWallet
{
    public class MB
    {
        [JsonProperty("entity")]
        public string Entity { get; set; }

        [JsonProperty("ref")]
        public string Ref { get; set; }
    }
}