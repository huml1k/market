using System;
using System.Collections.Generic;
using System.Text;

namespace Market.Contracts.Requests.Products
{
    public record UpdateProductRequest(Guid Id, string Name, decimal Price);
}
