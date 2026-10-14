using Market.Contracts.Requests.OrderItems;

namespace Market.Contracts.Requests.Orders
{
    public record UpdateOrderRequest(Guid Id, string Address, List<OrderItemsRequest> Products);
}
