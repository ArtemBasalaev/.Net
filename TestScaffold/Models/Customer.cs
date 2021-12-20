using System;
using System.Collections.Generic;

#nullable disable

namespace TestScaffold.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string FistName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public DateTime? BirthDate { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
