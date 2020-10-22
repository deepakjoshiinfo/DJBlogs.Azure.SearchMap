using Azure.Search.Documents.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DJBlogs.Azure.Search.Models
{
    public class Location
    {
        public string type { get; set; }

        [SearchableField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public double[] coordinates { get; set; }
    }
}
