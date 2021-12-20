using System;
using System.Collections.Generic;

#nullable disable

namespace TestScaffold.Models
{
    public partial class Category
    {
        public Category()
        {
            ProductCategoryLists = new HashSet<ProductCategoryList>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProductCategoryList> ProductCategoryLists { get; set; }
    }
}
