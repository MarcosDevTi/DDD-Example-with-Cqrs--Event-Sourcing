using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace DDDExample.SharedKernel.Paging
{
    public class PagedResult<T> : IEnumerable<T>
    {
        public PagedResult(IEnumerable<T> items, int totalNumberOfItems, Paging<T> paging)
        {
            Items = items.ToList();
            TotalNumberOfItems = totalNumberOfItems;
            Paging = paging;
        }

        [DataMember]
        public IReadOnlyList<T> Items { get; private set; }

        public Paging<T> Paging { get; private set; }

        [DataMember]
        public int TotalNumberOfItems { get; private set; }

        public IEnumerator<T> GetEnumerator() => Items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Items.GetEnumerator();
    }
}
