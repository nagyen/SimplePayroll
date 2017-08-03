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

        // function to calculate income after deducting fed tax
        public decimal CalculateIncomeAfterFedTax(Employee employee, decimal pay)
        {
            // calculate taxable income

            // reduce Insurance and 401k Pre tax
            var taxableIncome = pay - employee.Insurance;
            if(employee.Retirement401kPreTax)
            {
                taxableIncome = taxableIncome - (taxableIncome * employee.Retirement401kPercent);
            }

            // reduce w4 withholding allowances
            taxableIncome = taxableIncome - (decimal)(employee.W4Allowances * 168.8);

			// if taxable income < 500 no tax
			if(taxableIncome < 500)
            {
                return taxableIncome;
            }

            // if taxable income > 500 && < 5000 = 3% tax
            if(taxableIncome > 500 && taxableIncome < 5000)
            {
                return taxableIncome - (taxableIncome * (decimal)0.3);
            }

			// if taxable income >= 5000 && < 10,000 = 7% tax
            if (taxableIncome >= 5000 && taxableIncome < 10000)
			{
				return taxableIncome - (taxableIncome * (decimal)0.7);
			}

            // everything else deduct standard tax percent
            using(var db = new AppDbContext())
            {
                var standardFedTax = db.TaxPercentages.First(x => x.TaxCode == "FED").Percent;
                return taxableIncome - (taxableIncome * standardFedTax);
            }
        }
    }
}
