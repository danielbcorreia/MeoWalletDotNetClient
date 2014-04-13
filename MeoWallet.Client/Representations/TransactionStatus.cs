using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MeoWallet
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TransactionStatus
    {
        [EnumMember(Value = "NEW")]
        New,

        [EnumMember(Value = "COMPLETED")]
        Completed,

        [EnumMember(Value = "PENDING")]
        Pending,

        [EnumMember(Value = "FAIL")]
        Fail,

        // TODO undocumented in structure, appears in the DELETE checkout operation
        [EnumMember(Value = "VOIDED")]
        Voided
    }
}