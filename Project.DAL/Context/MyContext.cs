using Project.DAL.StrategyPattern;
using Project.ENTITIES.Models;
using Project.MAP.Options;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Context
{
    public class MyContext:DbContext
    {
        public MyContext():base("myConnection")
        {
            Database.SetInitializer(new MyInit());

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new CheckMap());
            modelBuilder.Configurations.Add(new OrderDetailMap());
            modelBuilder.Configurations.Add(new OrderMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new RebateMap());
            modelBuilder.Configurations.Add(new SafeMap());
            modelBuilder.Configurations.Add(new CompanyCardMap());
            modelBuilder.Configurations.Add(new UserCardMap());

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Check>Checks { get; set; }
        public DbSet<OrderDetail>OrderDetails { get; set; }
        public DbSet<Order>Orders { get; set; }
        public DbSet<Product>Products { get; set; }
        public DbSet<Rebate>Rebates { get; set; }
        public DbSet<Safe>Saves { get; set; }
        public DbSet<CompanyCard>CompanyCards { get; set; }
        public DbSet<UserCard>UserCards { get; set; }





    }
}
