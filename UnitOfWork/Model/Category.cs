﻿using System.Collections.Generic;

namespace UnitOfWork.Model
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public virtual List<ProductCategory> ProductCategories { get; set; } = new();
    }
}