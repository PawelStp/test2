using System.Collections.Generic;

namespace Games.Api.Models
{
    public abstract class PageBaseResult<T> where T : new()
    {
        public int Count { get; set; }
        public int PageIndex { get; set; }
        public IList<T> Data { get; set; }
    }
}
