using System;
using System.Collections.Generic;

#nullable disable

namespace TestScaffold.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
            ProductCategoryLists = new HashSet<ProductCategoryList>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ProductCategoryList> ProductCategoryLists { get; set; }
    }
}
