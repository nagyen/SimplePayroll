using System;
using System.Linq;
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
	        SeedPayments();
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

        private static void SeedPayments()
        {
	        var random = new Random();
	        var taxService = new TaxCalculationService();
            using(var db = new AppDbContext())
            {
	            var employees = db.Employees.ToList();
	            foreach (var employee in employees)
	            {
		            for (var i = 59; i > 1; i = i-2)
		            {
			            var grosspay = (decimal) Math.Round(random.NextDouble() * 1000, 2);
			            var deductions = taxService.GetDeductions(employee, grosspay);
			            var payment = new Payment
			            {
				            EmpId = employee.Id,
				            GrossPay = grosspay,
				            PaymentPeriodFrom = DateTime.Now.AddDays(-i * 7 + 1),
				            PaymentPeriodTo = DateTime.Now.AddDays(-(i-2) * 7),
				            FedTax = deductions.FedTax,
				            StateTax = deductions.StateTax,
				            Insurance = deductions.Insurance,
				            SocialSecurityTax = deductions.SocialSecurityTax,
				            MedicareTax = deductions.MedicareTax,
				            Retirement401K = deductions.Retirement401K,
				            NetPay = deductions.NetPay,
				            CreateDateTime = DateTime.Now.AddDays((-(i-2)*7) + 1)
			            };
			            // add only if valid payment
			            if (payment.NetPay > 0)
			            {
				            db.Payments.Add(payment);
			            }
		            }
	            }
	            db.SaveChanges();
            }
        }
    }
}
