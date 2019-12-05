namespace CurrencyConverter.Context
{
    using CurrencyConverter.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class FinanceContext : DbContext
    {
        
        public FinanceContext()
            : base("name=FinanceContext")
        {
        }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Course> Currencs { get; set; }
    }

    
}