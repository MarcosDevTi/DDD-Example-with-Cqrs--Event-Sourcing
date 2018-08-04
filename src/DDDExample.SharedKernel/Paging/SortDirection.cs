using System.Runtime.Serialization;

namespace DDDExample.SharedKernel.Paging
{
    [DataContract]
    public enum SortDirection
    {
        [EnumMember]
        Ascending,

        [EnumMember]
        Descending
    }
}
