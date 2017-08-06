namespace core.Models
{
    public class TaxModels
    {
        public class Deductions
        {
            public decimal TaxableIncome { get; set; }
            public decimal Retirement401K { get; set; }
            public decimal Insurance { get; set; }
            public decimal W4Allowances { get; set; }
            public decimal FedTax { get; set; }
            public decimal StateTax { get; set; }
            public decimal SocialSecurityTax { get; set; }
            public decimal MedicareTax { get; set; }
            public decimal NetPay { get; set; }
        }
    }
}