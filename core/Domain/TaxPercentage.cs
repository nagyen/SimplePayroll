using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace core.Domain
{
    [Table("tTaxPercentage", Schema = "dbo")]
    public class TaxPercentage
    {
        [Key]
        public long Id { get; set; }
        public string TaxCode { get; set; }
        public decimal Percent { get; set; }
    }
}
