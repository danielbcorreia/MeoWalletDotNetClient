using System.Runtime.Serialization;

namespace MeoWallet
{
    public enum RefundType
    {
        [EnumMember(Value = "FULL")]
        Full,
        [EnumMember(Value = "PARTIAL")]
        Partial
    }
}