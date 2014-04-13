using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MeoWallet
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PaymentMethod
    {
        [EnumMember(Value = "CC")]
        CreditCard,

        [EnumMember(Value = "MB")]
        Multibanco,

        [EnumMember(Value = "WALLET")]
        Wallet,

        [EnumMember(Value = "PAYPAL")]
        PayPal
    }
}