using System.Runtime.Serialization;

namespace Domain.Entities
{
    public enum Status
    {
        [EnumMember(Value = "Passive")]
        Passive = 0,

        [EnumMember(Value = "Available")]
        Available = 1
    }
}
