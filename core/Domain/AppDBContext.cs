using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace core.Domain
{
    public class AppDbContext: DbContext
    {
        // configure in-memory database
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("SimplePayroll");
        }

        // define domain tables
        public DbSet<Authentication> Auths { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<TaxPercentage> TaxPercentages { get; set; }
    }
}
