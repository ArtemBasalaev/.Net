using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShopDataBase.Entities;

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
            { FistName = "Иван", LastName = "Иванов", Phone = "2835645", Mail = "ivan@mail.ru" });
            db.Customers.Add(new Customer
            { FistName = "Петр", LastName = "Петров", Phone = "2548563", Mail = "petr@mail.ru" });
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

            // запрос 1: вариант 1
            var mostPopularProduct = db.Products
                .OrderByDescending(p => p.OrderDetailsList.Count)
                .Select(p => p.Name)
                .FirstOrDefault();

            if (mostPopularProduct != null)
            {
                Console.WriteLine($"Наиболее популярный продукт: {mostPopularProduct}");
            }

            // запрос 1: вариант 2
            var products = db.Products.ToList();

            var ordersMaxCountByProducts = products
                .Max(p => p.OrderDetailsList.Count);

            var mostPopularProducts = products
                .Where(p => p.OrderDetailsList.Count == ordersMaxCountByProducts)
                .Select(p => p.Name);

            Console.WriteLine("Наиболее популярный продукт: ");

            foreach (var product in mostPopularProducts)
            {
                Console.WriteLine(product);
            }

            // запрос 1: вариант 3
            var mostPopularProduct1 = products
                .OrderByDescending(p => p.OrderDetailsList.Count)
                .Select(p => p.Name)
                .FirstOrDefault();

            if (mostPopularProduct1 != null)
            {
                Console.WriteLine($"Наиболее популярный продукт: {mostPopularProduct1}");
            }

            // запрос 2
            var ordersTotalSumByClients = db.Customers
                            .Include(c => c.Orders)
                                .ThenInclude(o => o.OrderDetailsList)
                            .ToDictionary(c => c, c => c.Orders
                                .Select(o => o.OrderDetailsList
                                    .Sum(od => od.Product.Price))
                                .Sum());

            Console.WriteLine("Общая сумма заказов по клиентам:");

            foreach (var c in ordersTotalSumByClients)
            {
                Console.WriteLine($"{c.Key.FistName} {c.Key.LastName}: {c.Value} руб.");
            }

            // запрос 3
            var ordersTotalSumByCategories = db.Categories
                .Include(c => c.ProductCategories)
                    .ThenInclude(pc => pc.Product)
                .ToDictionary(c => c.Name, c => c.ProductCategories
                    .Select(pc => pc.Product.OrderDetailsList.Count)
                    .Sum());

            Console.WriteLine("Итого продаж по категориям:");

            foreach (var c in ordersTotalSumByCategories)
            {
                Console.WriteLine($"{c.Key} - {c.Value} шт.");
            }
        }
    }
}