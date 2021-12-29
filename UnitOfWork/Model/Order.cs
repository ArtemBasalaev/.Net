using System;
using System.Collections.Generic;

namespace UnitOfWork.Model
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual List<OrderDetails> OrderDetailsList { get; set; } = new();
    }
}