using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace UnitOfWork.Model
{
    public class ProductRepository : BaseEfRepository<Product>, IProductRepository
    {
        public ProductRepository(DbContext db) : base(db)
        {
        }

        public Product GetMostPopularProduct()
        {
            var mostPopularProduct = DbSet
                .Include(p => p.OrderDetailsList)
                .OrderByDescending(p => p.OrderDetailsList.Count)
                .Take(1)
                .SingleOrDefault();

            return mostPopularProduct;
        }
    }
}
