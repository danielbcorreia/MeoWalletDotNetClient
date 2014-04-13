using Newtonsoft.Json;

namespace MeoWallet
{
    public class OperationList
    {
        [JsonProperty("elements")]
        public Operation[] Elements { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }
    }
}