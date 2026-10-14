using System.Diagnostics.Contracts;
using System.Text.Json.Serialization;

namespace Market.DataBase.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        [JsonIgnore]
        public List<OrderItem> OrderItems { get; set; } 
    }
}
