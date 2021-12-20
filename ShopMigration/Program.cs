using System;
using Microsoft.EntityFrameworkCore;
using ShopMigration.Entities;

namespace ShopMigration
{
    public class Program
    {
        public static void Main()
        {
            using var db = new ShopContext();

            db.Database.Migrate();

            AddData(db);
        }

        private static void AddData(ShopContext db)
        {
            db.Categories.Add(new Category { Name = "бытовая электроника" });
            db.Categories.Add(new Category { Name = "аудио" });

            db.Products.Add(new Product { Name = "наушники HD569", Price = 7250 });
            db.Products.Add(new Product { Name = "наушники HD599", Price = 11500 });
            db.Products.Add(new Product { Name = "усилитель HD-20", Price = 35620 });
            db.Products.Add(new Product { Name = "LCD-телевизор Lg-32hd", Price = 32250 });
            db.Products.Add(new Product { Name = "LCD-телевизор Samsung-32hd", Price = 25550 });
            db.SaveChanges();

            db.ProductsCategoriesList.Add(new ProductCategory { ProductId = 1, CategoryId = 1 });
            db.ProductsCategoriesList.Add(new ProductCategory { ProductId = 1, CategoryId = 2 });
            db.ProductsCategoriesList.Add(new ProductCategory { ProductId = 2, CategoryId = 1 });
            db.ProductsCategoriesList.Add(new ProductCategory { ProductId = 2, CategoryId = 2 });
            db.ProductsCategoriesList.Add(new ProductCategory { ProductId = 3, CategoryId = 1 });
            db.ProductsCategoriesList.Add(new ProductCategory { ProductId = 3, CategoryId = 2 });
            db.ProductsCategoriesList.Add(new ProductCategory { ProductId = 4, CategoryId = 1 });
            db.ProductsCategoriesList.Add(new ProductCategory { ProductId = 5, CategoryId = 1 });
            db.SaveChanges();

            db.Customers.Add(new Customer
            {
                FistName = "Иван",
                LastName = "Иванов",
                Phone = "2835645",
                Mail = "ivan@mail.ru",
                BirthDate = new DateTime(1980, 7, 25)
            });

            db.Customers.Add(new Customer
            {
                FistName = "Петр",
                LastName = "Петров",
                Phone = "2548563",
                Mail = "petr@mail.ru",
                BirthDate = new DateTime(1989, 2, 15)
            });

            db.SaveChanges();

            db.Orders.Add(new Order { Date = DateTime.Now, CustomerId = 1 });
            db.Orders.Add(new Order { Date = DateTime.Now, CustomerId = 2 });
            db.Orders.Add(new Order { Date = DateTime.Now, CustomerId = 2 });
            db.SaveChanges();

            db.OrderDetailsList.Add(new OrderDetails { OrderId = 1, ProductId = 2 });
            db.OrderDetailsList.Add(new OrderDetails { OrderId = 1, ProductId = 3 });
            db.OrderDetailsList.Add(new OrderDetails { OrderId = 1, ProductId = 5 });
            db.OrderDetailsList.Add(new OrderDetails { OrderId = 2, ProductId = 1 });
            db.OrderDetailsList.Add(new OrderDetails { OrderId = 2, ProductId = 3 });
            db.OrderDetailsList.Add(new OrderDetails { OrderId = 2, ProductId = 4 });
            db.OrderDetailsList.Add(new OrderDetails { OrderId = 3, ProductId = 2 });
            db.SaveChanges();
        }
    }
}