using Newtonsoft.Json;

namespace MeoWallet
{
    public class Checkout
    {
        // out
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("url_redirect")]
        public string UrlRedirect { get; set; }

        // in / out
        [JsonProperty("payment")]
        public Operation Payment { get; set; }

        [JsonProperty("url_cancel")]
        public string UrlCancel { get; set; }

        [JsonProperty("url_confirm")]
        public string UrlConfirm { get; set; }

        // in
        [JsonProperty("required_fields")]
        public RequiredFields RequiredFields { get; set; }
        
        [JsonProperty("exclude")]
        public PaymentMethod[] Exclude { get; set; }

        // TODO undocumented
        [JsonProperty("allows_punch_card_stamp")]
        public bool AllowsPunchCardStamp { get; set; }
    }
}