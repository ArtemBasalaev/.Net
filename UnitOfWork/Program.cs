using System;
using UnitOfWork.Model;
using UnitOfWork.UoW;

namespace UnitOfWork
{
    public class Program
    {
        public static void Main()
        {
            using var uow = new UoW.UnitOfWork(new ShopContext());

            var productRepository = uow.GetRepository<IProductRepository>();
            var categoryRepository = uow.GetRepository<ICategoryRepository>();
            var productCategoryRepository = uow.GetRepository<IProductCategoryRepository>();

            categoryRepository.Create(new Category { Name = "Бытовая техника" });
            productRepository.Create(new Product { Name = "Утюг Tefal", Price = 5600 });

            uow.Save();

            productCategoryRepository.Create(new ProductCategory { CategoryId = 4, ProductId = 6 });

            uow.Save();

            var mostPopularProduct = productRepository.GetMostPopularProduct();

            if (mostPopularProduct != null)
            {
                Console.WriteLine($"Наиболее популярный продукт: {mostPopularProduct.Name}");
            }

            var mostUnpopularProduct = productRepository.GetMostUnpopularProduct();

            if (mostUnpopularProduct != null)
            {
                Console.WriteLine($"Наименее популярный продукт: {mostUnpopularProduct.Name}");
            }
        }
    }
}