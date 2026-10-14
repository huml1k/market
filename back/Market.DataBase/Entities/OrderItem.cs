using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Market.DataBase.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }
        [JsonIgnore]
        public Order Order { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
