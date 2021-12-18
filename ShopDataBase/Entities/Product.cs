using System.Collections.Generic;

namespace ShopDataBase.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public virtual List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

        public virtual List<OrderDetails> OrderDetailsList { get; set; } = new List<OrderDetails>();
    }
}