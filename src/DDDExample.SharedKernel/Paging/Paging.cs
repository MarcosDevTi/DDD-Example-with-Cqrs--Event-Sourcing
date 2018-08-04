using System.Runtime.Serialization;

namespace DDDExample.SharedKernel.Paging
{
    [DataContract]
    public class Paging<T>
    {
        public Paging() : this(0, int.MaxValue) { }

        public Paging(int skip, int top)
        {
            SortDirection = SortDirection.Ascending;
            Skip = skip;
            Top = top;
        }

        [DataMember]
        public SortDirection SortDirection { get; set; }

        [DataMember]
        public string SortColumn { get; set; }

        [DataMember]
        public int Skip { get; set; }

        [DataMember]
        public int Top { get; set; }

        public override string ToString() => $"SortColumn: {SortColumn}, Top: {Top}, Skip: {Skip}";
    }
}
