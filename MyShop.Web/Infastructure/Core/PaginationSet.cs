using System.Collections.Generic;
using System.Linq;

namespace MyShop.Web.Infastructure.Core
{
    public class PaginationSet<T>
    {
        public IEnumerable<T> Items { set; get; }
        public int Page { set; get; }
        public int Count { get { return (Items != null) ? Items.Count() : 0; } }
        public int TotalPages { set; get; }
        public int TotalCount { set; get; }
    }
}