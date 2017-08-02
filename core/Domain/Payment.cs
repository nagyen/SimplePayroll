using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace core.Domain
{
    [Table("tPayment", Schema = "dbo")]
    public class Payment
    {
        [Key]
        public long Id { get; set; }
        public long EmpId { get; set; }
        public decimal GrossPay { get; set; }
        public decimal PaymentPeriodFrom { get; set; }
        public decimal PaymentPeriodTo { get; set; }
        public decimal FedTax { get; set; }
        public decimal StateTax { get; set; }
        public decimal SocialSecurityTax { get; set; }
        public decimal MedicareTax { get; set; }
        public decimal Insurance { get; set; }
        public decimal Retirement401k { get; set; }
        public decimal NetPay { get; set; }
        
        public DateTime CreateDateTime { get; set; }
    }
}
