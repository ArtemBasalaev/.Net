using System.Collections.Generic;

namespace ShopDataBase.Model
{
    public class Customer
    {
        public int Id { get; set; }

        public string FistName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Mail { get; set; }

        public virtual List<Order> Orders { get; set; } = new();
    }
}