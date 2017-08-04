using System;
using System.Collections.Generic;
using System.Text;

namespace core.Models
{
    public class LisitngModels
    {
        // employee listing request filters
        public class ListingRequest
        {
            // custom filter
            public long? EmpId { get; set; }
            public string State { get; set; }
            public DateTime? PayPostingFrom { get; set; }
            public DateTime? PayPostingTo { get; set; }

            // default filters
            public int Current { get; set; }
            public int RowCount { get; set; }
            public string SearchPhrase { get; set; }

            // sort
            public Dictionary<string, string> Sort { get; set; }
        }

        // result
        public class ListingResult
        {
            // stats
            public int Current { get; set; }
            public int RowCount { get; set; }
            public int Total { get; set; }

            // define rows
            public IEnumerable<ListingItem> Rows { get; set; }
        }

        // employee listing row
        public class ListingItem
        {
            public string FullName { get; set; }
            public string State { get; set; }
            public string LastPaymentDate { get; set; }
            public string LastPaymentAmount { get; set; }
            public string YtdPay { get; set; }
        }
    }
}
