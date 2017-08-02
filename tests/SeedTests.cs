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

            // user
            var user = AsyncHelpers.RunSync(() => new UserAuthenticationService().GetUser(1));
            Console.WriteLine($"{user.Username} - {user.Id} - {user.FirstName} - {user.CreateDateTime}");
            // taxes
            var taxes = AsyncHelpers.RunSync(() => new TaxCalculationService().GetTaxPercentages());
            foreach(var tax in taxes)
            {
                Console.WriteLine($"{tax.TaxCode} - {tax.Percent}");
            }
        }
    }
}
