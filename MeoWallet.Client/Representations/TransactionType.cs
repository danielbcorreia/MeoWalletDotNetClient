using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MeoWallet
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TransactionType
    {
        [EnumMember(Value = "PAYMENT")]
        Payment,

        [EnumMember(Value = "WALLETFUNDS")]
        WalletFunds,

        // TODO in the list operations example, appears as "USERTRANSF"
        [EnumMember(Value = "USERTRANSFER")]
        UserTransfer,

        [EnumMember(Value = "REFUND")]
        Refund
    }
}