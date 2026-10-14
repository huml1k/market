using Market.Contracts.Requests.Orders;
using Market.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Market.Services.Abstractions
{
    public interface IOrderService
    {
        public Task CreateOrder(CreateOrderRequest request, CancellationToken ct);
        public Task DeleteOrder(Guid id, CancellationToken ct);
        public Task Update(UpdateOrderRequest request, CancellationToken ct);
        public Task<List<Order?>> GetListOrder(CancellationToken ct);
        public Task<Order?> GetOrderById(Guid id, CancellationToken ct);
    }
}
