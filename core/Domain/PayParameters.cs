using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace core.Domain
{
    [Table("tPayParameters", Schema = "dbo")]
    public class PayParameters
    {
        [Key]
        public long Id { get; set; }
        public decimal FedTaxPercent { get; set; }
        public decimal StateTaxPercent { get; set; }
        public decimal SocialSecurityTaxPercent { get; set; }
        public decimal MedicareTaxPercent { get; set; }
    }
}
