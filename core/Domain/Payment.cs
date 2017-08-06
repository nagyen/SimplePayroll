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
        [ForeignKey("Employee")]
        public long EmpId { get; set; }
        public decimal GrossPay { get; set; }
        public DateTime PaymentPeriodFrom { get; set; }
        public DateTime PaymentPeriodTo { get; set; }
        public decimal FedTax { get; set; }
        public decimal StateTax { get; set; }
        public decimal SocialSecurityTax { get; set; }
        public decimal MedicareTax { get; set; }
        public decimal Insurance { get; set; }
        public decimal Retirement401K { get; set; }
        public decimal NetPay { get; set; }
        public DateTime CreateDateTime { get; set; }
        
        // link to employee
        public virtual Employee Employee { get; set; }
    }
}
