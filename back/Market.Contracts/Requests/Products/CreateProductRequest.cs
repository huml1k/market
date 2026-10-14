using System;
using System.Collections.Generic;
using System.Text;

namespace Market.Contracts.Requests.Products
{
    public record CreateProductRequest(string Name, decimal Price);
}
