using Newtonsoft.Json;

namespace MeoWallet
{
    public class PaymentItem
    {
        [JsonProperty("descr")]
        public string Description { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("qt")]
        public int? Quantity { get; set; }

        [JsonProperty("ref")]
        public int? Reference { get; set; }

        [JsonProperty("amount")]
        public double? Amount { get; set; }
    }
}