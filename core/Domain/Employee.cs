﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace core.Domain
{
    [Table("tEmployee", Schema = "dbo")]
    public class Employee
    {
        [Key]
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string State { get; set; }
        public string SSN { get; set; }
        public decimal Insurance { get; set; }
        public int W4Allowances { get; set; }
        public decimal Retirement401KPercent { get; set; }
        public bool Retirement401KPreTax { get; set; }
        public DateTime CreateDateTime { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}
