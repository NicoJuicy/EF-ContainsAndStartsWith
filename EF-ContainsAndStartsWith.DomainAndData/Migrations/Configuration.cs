namespace EF_ContainsAndStartsWith.DomainAndData.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EF_ContainsAndStartsWith.DomainAndData.Data.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EF_ContainsAndStartsWith.DomainAndData.Data.DataContext context)
        {

            var catCodes = new string[] { "A", "B", "C", "D", "E" };
            
            foreach (string cat in catCodes)
            {
                for (int i = 0; i < 5; i++)
                {
                    var baseString = new string((char) char.Parse(cat), i);
                    context.Categories.AddOrUpdate<Models.Category>(el => el.Code, new Models.Category()
                    {
                        Code =baseString,
                        Name= baseString
                    });
                }
            }
            context.SaveChanges();

            var productNames = new string[] {
                "shoes","scissors","gyms","sneakers","fork","spoon","knife","pen","glasses","prescription goggle","goggle","prescription glasses","blindevis.be prescription goggle","sapico.me"};

            var rand = new Random();
            foreach (var cat in context.Categories.ToList())
            {
                foreach (var productName in productNames)
                {
                    for (int i = 0; i < 250; i++)
                    {
                        var stockAmount = rand.Next(0, 200);
                        context.Products.AddOrUpdate<Models.Product>(el => el.Name, new Models.Product()
                        {
                            CategoryId = cat.Id,
                            inStockAmount = stockAmount,
                            isActive = (stockAmount % 2 == 0),
                            PriceExcl = rand.Next(0, 5000),
                            Name =string.Format("{0} {1} {2}",productName,cat.Name,i.ToString()) //@"{productName} {cat.Name} {i}"
                        });
                    }
                    context.SaveChanges();
                }
            }


            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
