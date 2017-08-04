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
    }
}
