using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using core.Models;
using core.Domain;

namespace core.Services
{
    public class TaxCalculationService
    {
        // get all taxe percentages
        public async Task<IEnumerable<TaxPercentage>> GetTaxPercentages()
        {
            using(var db = new AppDbContext())
            {
                return await db.TaxPercentages.ToListAsync();
            }
        }
    }
}
