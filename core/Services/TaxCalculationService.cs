using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using core.Models;
using core.Domain;

namespace core.Services
{
    public class TaxCalculationService
    {
        // get all tax percentages
        public async Task<IEnumerable<TaxPercentage>> GetTaxPercentages()
        {
            using(var db = new AppDbContext())
            {
                return await db.TaxPercentages.ToListAsync();
            }
        }

        // function to calculate taxable income for emplaoyee
        public decimal CalculateTaxableIncome(Employee employee, decimal grossPay)
        {
			// reduce Insurance
			var taxableIncome = grossPay - employee.Insurance;

            // reduce 401k Pre tax
			if (employee.Retirement401KPreTax)
			{
				taxableIncome = taxableIncome - Calculate401KAmount(employee, grossPay);
			}

			// reduce w4 withholding allowances
			taxableIncome = taxableIncome - CalculateW4AllowanceAmount(employee);

	        // return 0 if negative
	        return taxableIncome < 0 ? 0 : taxableIncome;

        }
		
	    // function to calculate w4 allowances amount
	    public decimal CalculateW4AllowanceAmount(Employee employee)
	    {
		    return Math.Round((decimal) (employee.W4Allowances * 168.8), 2);
	    }
	    
	    // function to calculate 401k savings amount
	    public decimal Calculate401KAmount(Employee employee, decimal grossPay)
	    {
		    return Math.Round(grossPay * employee.Retirement401KPercent * (decimal) 0.01, 2);
	    }

        // function to calculate Fed tax amount
        public decimal CalculateFedTaxAmount(decimal taxableIncome)
        {
			// if taxable income < 500 no tax
			if (taxableIncome < 500)
			{
				return 0;
			}

			// if taxable income > 500 && < 5000 = 3% tax
			if (taxableIncome > 500 && taxableIncome < 5000)
			{
				return Math.Round(taxableIncome * (decimal)0.03, 2);
			}

			// if taxable income >= 5000 && < 10,000 = 7% tax
			if (taxableIncome >= 5000 && taxableIncome < 10000)
			{
				return Math.Round(taxableIncome * (decimal)0.07, 2);
			}

			// everything else deduct standard fed tax percent
			using (var db = new AppDbContext())
			{
				var standardFedTax = db.TaxPercentages.First(x => x.TaxCode == "FED").Percent;
				return Math.Round(taxableIncome * standardFedTax * (decimal)0.01, 2);
			}
        }

        // function to calculate State tax amount
		public decimal CalculateStateTaxAmount(decimal taxableIncome, string stateCode)
		{
			// if taxable income < 500 no tax
			if (taxableIncome < 500)
			{
				return 0;
			}

			// if taxable income > 500 && < 5000 = 1% tax
			if (taxableIncome > 500 && taxableIncome < 5000)
			{
				return Math.Round(taxableIncome * (decimal)0.01, 2);
			}

			// if taxable income >= 5000 && < 10,000 = 1.5% tax
			if (taxableIncome >= 5000 && taxableIncome < 10000)
			{
				return Math.Round(taxableIncome * (decimal)0.015, 2);
			}

			// everything else deduct standard state tax percent
			using (var db = new AppDbContext())
			{
                var standardStateTax = db.TaxPercentages.First(x => x.TaxCode == stateCode).Percent;
				return Math.Round(taxableIncome * standardStateTax * (decimal)0.01, 2);
			}
		}

		// function to calculate Social security tax amount
		public decimal CalculateSocialTaxAmount(decimal grossPay)
		{
			// deduct standard social security tax percent
			using (var db = new AppDbContext())
			{
				var standardSocialTax = db.TaxPercentages.First(x => x.TaxCode == "SOCIAL").Percent;
                return Math.Round(grossPay * standardSocialTax * (decimal)0.01, 2);
			}
		}

		// function to calculate Medicare security tax amount
		public decimal CalculateMedicareTaxAmount(decimal grossPay)
		{
			// deduct standard social security tax percent
			using (var db = new AppDbContext())
			{
				var standardMedicareTax = db.TaxPercentages.First(x => x.TaxCode == "MEDICARE").Percent;
				return Math.Round(grossPay * standardMedicareTax * (decimal)0.01, 2);
			}
		}


        // calculate net pay after all deductions
        public decimal CalculateNetPay(Employee employee, decimal grosspay)
        {
			// calculate taxable income
			var taxableIncome = CalculateTaxableIncome(employee, grosspay);

			// calculate 401k amount
			var retirement = Calculate401KAmount(employee, grosspay);

            // calculate all taxes
            var fedTax = CalculateFedTaxAmount(taxableIncome);
            var stateTax = CalculateStateTaxAmount(taxableIncome, employee.State);
            var socialTax = CalculateSocialTaxAmount(grosspay);
            var medicareTax = CalculateMedicareTaxAmount(grosspay);

            // reduce all deductions
            var finalpay = grosspay - (fedTax + stateTax + socialTax + medicareTax + employee.Insurance + retirement);

            return Math.Round(finalpay, 2);
        }
    }
}
