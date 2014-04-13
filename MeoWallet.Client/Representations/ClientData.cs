using Newtonsoft.Json;

namespace MeoWallet
{
    public class ClientData
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("nif")]
        public string Nif { get; set; }

        [JsonProperty("address")]
        public ClientAddress Address { get; set; }
    }
}