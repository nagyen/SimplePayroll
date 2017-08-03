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
        public decimal CalculateTaxableIncome(Employee employee, decimal pay)
        {
			// reduce Insurance
			var taxableIncome = pay - employee.Insurance;

            // reduce 401k Pre tax
			if (employee.Retirement401kPreTax)
			{
				taxableIncome = taxableIncome - (pay * employee.Retirement401kPercent);
			}

			// reduce w4 withholding allowances
			taxableIncome = taxableIncome - (decimal)(employee.W4Allowances * 168.8);

            // return 
            return taxableIncome;
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
				return taxableIncome * (decimal)0.3;
			}

			// if taxable income >= 5000 && < 10,000 = 7% tax
			if (taxableIncome >= 5000 && taxableIncome < 10000)
			{
				return taxableIncome * (decimal)0.7;
			}

			// everything else deduct standard fed tax percent
			using (var db = new AppDbContext())
			{
				var standardFedTax = db.TaxPercentages.First(x => x.TaxCode == "FED").Percent;
				return taxableIncome * standardFedTax;
			}
        }

        // function to calculate income after deducting fed tax
        public decimal CalculateIncomeAfterFedTax(Employee employee, decimal pay)
        {
            // calculate taxable income
            var taxableIncome = CalculateTaxableIncome(employee, pay);

            // calculate fedtax amount for taxable income
            var fedTaxAmount = CalculateFedTaxAmount(taxableIncome);

            // deduct tax amount from pay
            return pay - fedTaxAmount;
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
				return taxableIncome * (decimal)1.0;
			}

			// if taxable income >= 5000 && < 10,000 = 1.5% tax
			if (taxableIncome >= 5000 && taxableIncome < 10000)
			{
				return taxableIncome * (decimal)1.5;
			}

			// everything else deduct standard state tax percent
			using (var db = new AppDbContext())
			{
                var standardFedTax = db.TaxPercentages.First(x => x.TaxCode == stateCode).Percent;
				return taxableIncome * standardFedTax;
			}
		}

		// function to calculate income after deducting state tax
		public decimal CalculateIncomeAfterStateTax(Employee employee, decimal pay)
		{
			// calculate taxable income
			var taxableIncome = CalculateTaxableIncome(employee, pay);

			// calculate employee's state tax amount for taxable income
            var stateTaxAmount = CalculateStateTaxAmount(taxableIncome, employee.State);

			// deduct tax amount from pay
			return pay - stateTaxAmount;
        }

		// function to calculate Social security tax amount
		public decimal CalculateSocialTaxAmount(decimal grossPay)
		{
			// deduct standard social security tax percent
			using (var db = new AppDbContext())
			{
				var standardSocialTax = db.TaxPercentages.First(x => x.TaxCode == "SOCIAL").Percent;
                return grossPay * standardSocialTax;
			}
		}

		// function to calculate income after deducting social security tax
		public decimal CalculateIncomeAfterSocialTax(decimal pay)
		{
			// calculate social tax amount for total income
			var socialTaxAmount = CalculateSocialTaxAmount(pay);

			// deduct tax amount from pay
			return pay - socialTaxAmount;
		}

		// function to calculate Medicare security tax amount
		public decimal CalculateMedicareTaxAmount(decimal grossPay)
		{
			// deduct standard social security tax percent
			using (var db = new AppDbContext())
			{
				var standardMedicareTax = db.TaxPercentages.First(x => x.TaxCode == "MEDICARE").Percent;
				return grossPay * standardMedicareTax;
			}
		}

		// function to calculate income after deducting social security tax
		public decimal CalculateIncomeAfterMedicareTax(decimal pay)
		{
			// calculate social tax amount for total income
			var medicareTaxAmount = CalculateMedicareTaxAmount(pay);

			// deduct tax amount from pay
			return pay - medicareTaxAmount;
		}

        // calculate final pay after all taxes
        public decimal CalculateFinalPayAfterDeductions(Employee employee, decimal grosspay)
        {
			// calculate taxable income
			var taxableIncome = CalculateTaxableIncome(employee, grosspay);

			// calculate 401k pretax
			decimal retirement = 0;
			if (employee.Retirement401kPreTax)
			{
                retirement = grosspay * employee.Retirement401kPercent;
			}

            // calculate all taxes
            var fedTax = CalculateFedTaxAmount(taxableIncome);
            var stateTax = CalculateStateTaxAmount(taxableIncome, employee.State);
            var socialTax = CalculateSocialTaxAmount(grosspay);
            var medicareTax = CalculateMedicareTaxAmount(grosspay);

            // deductions
            var finalpay = grosspay - (fedTax + stateTax + socialTax + medicareTax + employee.Insurance + retirement);

            return finalpay;
        }
    }
}
