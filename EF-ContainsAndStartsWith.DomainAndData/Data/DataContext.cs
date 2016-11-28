using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_ContainsAndStartsWith.DomainAndData.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
            this.Database.Log = el => System.Diagnostics.Debug.WriteLine(el);
        }
        public DataContext(bool onlyReading)
        {
           


            this.Configuration.LazyLoadingEnabled = false;// always include all entities for performance

            if (onlyReading)
            {
                this.Configuration.ProxyCreationEnabled = false;
                this.Configuration.AutoDetectChangesEnabled = false;
            }


        }

        public DbSet<Models.Product> Products { get; set; }
        public DbSet<Models.Category> Categories { get; set; }

        public override int SaveChanges()
        {
            //try catch if required
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync()
        {
            //try catch if required
            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
