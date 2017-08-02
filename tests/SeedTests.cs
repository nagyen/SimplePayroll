using System;
using core;
using core.Domain;
using core.Services;

namespace tests
{
    public class SeedTests
    {
        public static void Run()
        {
            SeedTables.Run();

            // taxes
            var taxes = AsyncHelpers.RunSync(() => new TaxCalculationService().GetTaxPercentages());
            foreach(var tax in taxes)
            {
                Console.WriteLine($"{tax.TaxCode} - {tax.Percent}");
            }
        }
    }
}
