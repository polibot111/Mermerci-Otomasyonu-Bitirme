using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus.DataSets;
using Project.COMMON.Tools;
using Project.DAL.Context;
using Project.ENTITIES.Models;

namespace Project.DAL.StrategyPattern
{
    public class MyInit : CreateDatabaseIfNotExists<MyContext>
    {
        protected override void Seed(MyContext context)
        {
            #region Admin
            UserCard uc = new UserCard();
            uc.UserName = "mustafa";
            uc.Password = DantexCrypt.Crypt("1234");
            uc.Email = "m.keklikcii7@gmail.com";
            uc.Role = ENTITIES.Enums.UserCardRole.Admin;

            context.UserCards.Add(uc);
            context.SaveChanges();

            CompanyCard cc = new CompanyCard();
            cc.ID = uc.ID;
            cc.CompanyName = "Star Mermerci";
            cc.Address = "Dağ mah. Tepe Sokak No:4";

            #endregion

            for (int i = 0; i < 50; i++)
            {
                UserCard uC = new UserCard();
                uC.UserName = new Internet("tr").UserName();
                uC.Password = new Internet("tr").Password();
                uC.Email = new Internet("tr").Email();

                context.UserCards.Add(uC);
                

            }
            context.SaveChanges();

            for (int i = 2; i < 52; i++)
            {
                CompanyCard ccMember = new CompanyCard();
                ccMember.ID = i;
                ccMember.CompanyName = new Name("tr").FirstName();
                ccMember.Address = new Address("tr").Locale;
                context.CompanyCards.Add(ccMember);

            }
            context.SaveChanges();

            for (int i = 0; i < 10; i++)
            {
                Category c = new Category();
                c.CategoryName = new Commerce("tr").Categories(1)[0];
                c.Description = new Lorem("tr").Sentence(10);

                for (int j = 0; j < 30; j++)
                {
                    Product p = new Product();
                    p.ProductName = new Commerce("tr").ProductName();
                    p.UnitPrice = Convert.ToDecimal(new Commerce("tr").Price());
                    p.UnitsInStock = 100;
                    p.ImagePath = new Images().Nightlife();

                }
                context.Categories.Add(c);
                context.SaveChanges();
            }

        }
    }
}

   