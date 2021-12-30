using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShopDataBase.Model;

namespace ShopDataBase
{
    public class Program
    {
        public static void Main()
        {
            using var db = new ShopContext();

            db.Database.EnsureDeleted();

            db.Database.EnsureCreated();

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
                FirstName = "Иван",
                LastName = "Иванов",
                Phone = "2835645",
                Email = "ivan@mail.ru"
            });

            db.Customers.Add(new Customer
            {
                FirstName = "Петр",
                LastName = "Петров",
                Phone = "2548563",
                Email = "petr@mail.ru"
            });

            db.SaveChanges();

            db.Orders.Add(new Order { Date = DateTime.UtcNow, CustomerId = 1 });
            db.Orders.Add(new Order { Date = DateTime.UtcNow, CustomerId = 2 });
            db.Orders.Add(new Order { Date = DateTime.UtcNow, CustomerId = 2 });
            db.SaveChanges();

            db.OrderDetailsList.Add(new OrderDetails { OrderId = 1, ProductId = 2, Quantity = 2 });
            db.OrderDetailsList.Add(new OrderDetails { OrderId = 1, ProductId = 3, Quantity = 1 });
            db.OrderDetailsList.Add(new OrderDetails { OrderId = 1, ProductId = 5, Quantity = 15 });
            db.OrderDetailsList.Add(new OrderDetails { OrderId = 2, ProductId = 1, Quantity = 1 });
            db.OrderDetailsList.Add(new OrderDetails { OrderId = 2, ProductId = 3, Quantity = 1 });
            db.OrderDetailsList.Add(new OrderDetails { OrderId = 2, ProductId = 4, Quantity = 1 });
            db.OrderDetailsList.Add(new OrderDetails { OrderId = 3, ProductId = 2, Quantity = 1 });
            db.SaveChanges();

            var product1 = db.Products.Find(1);
            product1.Price = 8250;
            db.SaveChanges();

            db.Categories.Add(new Category { Name = "компьютеры" });
            db.SaveChanges();

            var categoryToDelete = new Category { Id = 3 };
            db.Entry(categoryToDelete).State = EntityState.Deleted;
            db.SaveChanges();

            //var category = db.Categories.FirstOrDefault(c => c.Id == 3);

            //if (category != null)
            //{
            //    db.Categories.Remove(category);
            //    db.SaveChanges();
            //}

            var product2 = db.Products.FirstOrDefault(p => p.Name == "усилитель HD-20");

            if (product2 != null)
            {
                Console.WriteLine($"{product2.Name} - {product2.Price} руб.");
            }

            var limitPrice = 15000;

            var cheapestProductsList = db.Products
                .Where(p => p.Price < limitPrice)
                .Select(p => p.Name);

            Console.WriteLine("Самые дешевые товары:");

            foreach (var name in cheapestProductsList)
            {
                Console.WriteLine("- " + name);
            }

            //запрос 1: вариант 1
            var mostPopularProduct = db.Products
                .OrderByDescending(p => p.OrderDetailsList.Sum(od => od.Quantity))
                .Select(p => p.Name)
                .FirstOrDefault();

            if (mostPopularProduct != null)
            {
                Console.WriteLine($"Наиболее популярный продукт: {mostPopularProduct}");
            }

            //запрос 1: вариант 2
            var maxProductUnitsCountSold = db.OrderDetailsList
                .GroupBy(od => od.Product.Id)
                .Max(g => g.Sum(od => od.Quantity));

            var mostPopularProducts = db.Products
                .Where(p => p.OrderDetailsList.Sum(od => od.Quantity) == maxProductUnitsCountSold)
                .Select(p => p.Name);

            Console.WriteLine("Наиболее популярные продукты:");

            foreach (var product in mostPopularProducts)
            {
                Console.WriteLine($"{product}");
            }

            //запрос 2
            var ordersTotalSumByClients = db.Customers
                .Select(c => new
                {
                    Customer = c,
                    OrdersSum = c.Orders.SelectMany(o => o.OrderDetailsList.Select(od => od.Quantity * od.Product.Price)).Sum()
                })
                .ToList();

            Console.WriteLine("Общая сумма заказов по клиентам:");

            foreach (var c in ordersTotalSumByClients)
            {
                Console.WriteLine($"{c.Customer.LastName}: {c.OrdersSum}");
            }

            // запрос 3
            var unitsSalesByCategories = db.Categories
                .Select(c => new
                {
                    CategoryName = c.Name,
                    UnitsSalesByCategory = c.ProductCategories.SelectMany(o => o.Product.OrderDetailsList.Select(od => od.Quantity)).Sum()
                })
                .ToList();

            Console.WriteLine("Итоги продаж по категориям:");

            foreach (var c in unitsSalesByCategories)
            {
                Console.WriteLine($"{c.CategoryName} - {c.UnitsSalesByCategory} шт.");
            }
        }
    }
}