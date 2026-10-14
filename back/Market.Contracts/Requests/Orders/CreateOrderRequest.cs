using Market.Contracts.Requests.OrderItems;

namespace Market.Contracts.Requests.Orders
{
    public record CreateOrderRequest(string Address, List<OrderItemsRequest> Products);
}
