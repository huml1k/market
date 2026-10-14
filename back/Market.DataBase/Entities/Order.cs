using System;
using System.Collections.Generic;
using System.Text;

namespace Market.DataBase.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid NumberId { get; set; }
        public string Address { get; set; }

        public List<OrderItem> OrderItems { get; set; } 
    }
}
