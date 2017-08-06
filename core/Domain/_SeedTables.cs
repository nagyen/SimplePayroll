using System;
using core.Services;

namespace core.Domain
{
    public class SeedTables
    {
        public static void Run()
        {
            SeedTaxPercentages();
            SeedUser();
            SeedEmployees();
        }

        private static void SeedTaxPercentages()
        {
            using(var db = new AppDbContext())
            {
				// define tax record
				var taxPercentage = new TaxPercentage();

				// fed tax
				taxPercentage.Id = 0;
				taxPercentage.TaxCode = "FED";
				taxPercentage.Percent = 12;
                db.TaxPercentages.Add(taxPercentage);
                db.SaveChanges();

				// social security tax
				taxPercentage.Id = 0;
				taxPercentage.TaxCode = "SOCIAL";
                taxPercentage.Percent = (decimal)6.2;
				db.TaxPercentages.Add(taxPercentage);
				db.SaveChanges();

				// medicare tax
				taxPercentage.Id = 0;
				taxPercentage.TaxCode = "MEDICARE";
                taxPercentage.Percent = (decimal)1.45;
				db.TaxPercentages.Add(taxPercentage);
				db.SaveChanges();

				// state tax
				var random = new Random();
                foreach(var state in SeedingHelpers.StatesGenerator.Abbreviations())
                {
                    taxPercentage.Id = 0;
                    taxPercentage.TaxCode = state;
                    taxPercentage.Percent = (decimal)Math.Round(random.NextDouble() * 10, 2);
					db.TaxPercentages.Add(taxPercentage);
					db.SaveChanges();
                }
			}
        }

        // seed user
        private static void SeedUser()
        {
            var user = new User
            {
                Username = "admin",
                Password = "admin",
                FirstName = "Admin",
                LastName = ""
            };

            AsyncHelpers.RunSync(() => new UserAuthenticationService().Register(user));
        }

        private static void SeedEmployees()
        {
            var random = new Random();
            using (var db = new AppDbContext())
            {
                for (var i = 0; i < 50; i++)
                {
                    // create new employee
                    var employee = new Employee
                    {
                        FirstName = SeedingHelpers.NameGenerator.GenRandomFirstName(),
                        LastName = SeedingHelpers.NameGenerator.GenRandomLastName(),
                        State = SeedingHelpers.StatesGenerator.GetRandomState().Abbreviation,
                        W4Allowances = random.Next(0, 4),
                        SSN = SeedingHelpers.SSNGenerator.UniqueSSNs[i],
                        Insurance = (decimal)Math.Round(random.NextDouble() * 50, 0),
                        Retirement401KPreTax = random.NextDouble() > 0.5,
                        Retirement401KPercent = (decimal)Math.Round(random.NextDouble() * 6, 0),
                        CreateDateTime = SeedingHelpers.DateGenerator.GetRandomDate(DateTime.Now.AddYears(-5), DateTime.Now)
                    };
                    // add to table
                    db.Employees.Add(employee);
                }
                // commit
                db.SaveChanges();
            }
        }

//        private static void SeedPayments()
//        {
//            using(var db = new AppDbContext())
//            {
//			    var employees = 
//
//			}
//        }
    }
}
