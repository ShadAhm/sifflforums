using System;
using System.Collections.Generic;
using System.Text;

namespace SifflForums.Service.Models
{
    public class PaginatedListResult<T>
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}
