using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MeoWallet
{
    public class Operation
    {
        // out
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public TransactionType? Type { get; set; }

        [JsonProperty("status")]
        public TransactionStatus? Status { get; set; }

        [JsonProperty("method")]
        public PaymentMethod? Method { get; set; }

        // TODO language bug in docs
        [JsonProperty("ipaddress")]
        public string IpAddress { get; set; }

        [JsonProperty("amount_net")]
        public double? AmountNet { get; set; }

        [JsonProperty("fee")]
        public double? Fee { get; set; }

        // date format: ISO 8601 YYYY-MM-DDThh:mm:ssTZD
        [JsonProperty("date"), JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime? Date { get; set; }

        [JsonProperty("card")]
        public CreditCard Card { get; set; }

        [JsonProperty("mb")]
        public MB Multibanco { get; set; }

        [JsonProperty("origin")]
        public User Origin { get; set; }

        [JsonProperty("destination")]
        public User Destination { get; set; }

        [JsonProperty("merchant")]
        public Merchant Merchant { get; set; }

        [JsonProperty("parent")]
        public string Parent { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        // TODO undocumented
        [JsonProperty("user_notes")]
        public string UserNotes { get; set; }

        // TODO undocumented, WEBSITE
        [JsonProperty("channel")]
        public string Channel { get; set; }

        // TODO undocumented
        [JsonProperty("refundable")]
        public bool? Refundable { get; set; }

        // TODO undocumented
        [JsonProperty("shipping")]
        public bool? Shipping { get; set; }

        // in / out

        [JsonProperty("amount")]
        public double Amount { get; set; }

        // in

        [JsonProperty("client")]
        public ClientData ClientData { get; set; }

        // currency code as defined in ISO 4217
        [JsonProperty("currency")]
        public ISO4217CurrencyCodes Currency { get; set; }

        [JsonProperty("items")]
        public PaymentItem[] Items { get; set; }

        [JsonProperty("store")]
        public long? Store { get; set; }

        [JsonProperty("ext_invoiceid")]
        public string ExtInvoiceId { get; set; }

        // TODO language bug in docs
        [JsonProperty("ext_costumerid")]
        public string ExtCustomerId { get; set; }

        [JsonProperty("ext_employee")]
        public string ExtEmployee { get; set; }
    }
}