using System;
using Newtonsoft.Json;

namespace MeoWallet
{
    public class Callback
    {
        [JsonProperty("operation_id")]
        public Guid OperationId { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }

        // currency as in ISO 4217
        [JsonProperty("currency")]
        public ISO4217CurrencyCodes Currency { get; set; }

        [JsonProperty("ext_invoiceid")]
        public string ExtInvoiceId { get; set; }

        [JsonProperty("ext_customerid")]
        public string ExtCustomerId { get; set; }

        // TODO undocumented
        [JsonProperty("ext_email")]
        public string ExtEmail { get; set; }

        // TODO undocumented
        [JsonProperty("user")]
        public long User { get; set; }

        // TODO its an Enum, but the allowed values are not documented
        [JsonProperty("event")]
        public string Event { get; set; }

        // TODO assuming type PaymentStatus
        [JsonProperty("operation_status")]
        public TransactionStatus OperationStatus { get; set; }

        [JsonProperty("method")]
        public PaymentMethod PaymentMethod { get; set; }

        [JsonProperty("mb")]
        public MB MB { get; set; }

        [JsonProperty("cc")]
        public CreditCard CreditCard { get; set; }
    }
}