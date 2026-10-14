using System;
using System.Collections.Generic;
using System.Text;

namespace Market.Contracts.Requests.OrderItems
{
    public record OrderItemsRequest(Guid ProductId, int Quantity);
}
