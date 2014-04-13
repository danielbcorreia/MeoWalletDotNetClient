using Newtonsoft.Json;

namespace MeoWallet
{
    public class RefundRequest
    {
        [JsonProperty("type")]
        public RefundType? Type { get; set; }

        [JsonProperty("amount")]
        public double? Amount { get; set; }

        [JsonProperty("ext_invoiceid")]
        public string ExtInvoiceId { get; set; }

        [JsonProperty("ext_email")]
        public string ExtEmail { get; set; }

        [JsonProperty("ext_customerid")]
        public string ExtCustomerId { get; set; }
    }
}