﻿namespace MyShop.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MyShop.Data.MyShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MyShopDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            CreateProductCategoriesSample(context);


            //var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new MyShopDbContext()));
            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new MyShopDbContext()));

            //var user = new ApplicationUser()
            //{
            //    UserName = "long",
            //    Email = "long@gmail.com",
            //    BirthDay = DateTime.Now,
            //    EmailConfirmed = true,
            //    FullName = "le long"
            //};

            ////Add new user
            //userManager.Create(user, "12345@");
            ////Add new roles
            //if (!roleManager.Roles.Any())
            //{
            //    roleManager.Create(new IdentityRole { Name = "Admin" });
            //    roleManager.Create(new IdentityRole { Name = "User" });
            //}

            ////Add user-roles
            //var adminUser = userManager.FindByEmail("long@gmail.com");
            //userManager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
        }

        private void CreateProductCategoriesSample(MyShopDbContext context)
        {
            if (context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> lstProductCategory = new List<ProductCategory>()
                {
                    new ProductCategory(){Name="Đồ điện lạnh", Alias="do-dien-lanh", Status=true },
                    new ProductCategory(){Name="Đồ điện tử", Alias="do-dien-tu", Status=true },
                    new ProductCategory(){Name="Đồ gia dụng", Alias="do-gia-dung", Status=true },
                    new ProductCategory(){Name="Đồ thời trang", Alias="do-thoi-trang", Status=true }
                };
                context.ProductCategories.AddRange(lstProductCategory);
                context.SaveChanges();
            }
        }
    }
}