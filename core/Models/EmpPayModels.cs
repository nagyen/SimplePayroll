using System;
using System.Collections.Generic;
using System.Text;
using core.Domain;

namespace core.Models
{
    public class EmpPayModels
    {
        // Employee Add/Update Feedback
        public class EmpAddUpdateFeedback
        {
            public long EmpId { get; set; }
            public string Errors { get; set; }
            public bool Success { get; set; }
        }
        
        // Record Payment feedback
        public class PaymentFeedback
        {
            public long EmpId { get; set; }
            public string Errors { get; set; }
            public bool Success { get; set; }
        }

        // employee listing request filters
        public class ListingRequest
        {
            // custom filter
            public long EmpId { get; set; }

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
            public IEnumerable<EmplListingItem> Rows { get; set; }
        }

        // employee listing row
        public class EmplListingItem
        {
            public string EmplFullName { get; set; }
            public string LastPaymentDate { get; set; }
            public string LastPaymentAmount { get; set; }
        }
    }
}
