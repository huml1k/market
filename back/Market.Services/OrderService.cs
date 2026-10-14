using Market.Contracts.Requests.Orders;
using Market.DataBase.Abstractions.Repositories;
using Market.DataBase.Entities;
using Market.Services.Abstractions;
using Microsoft.Extensions.Logging;

namespace Market.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly ILogger<OrderService> _logger;

        public OrderService(
            IRepository<Order> orderRepository,
            ILogger<OrderService> logger) 
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }

        public async Task CreateOrder(CreateOrderRequest request, CancellationToken ct)
        {
            try
            {
                var order = new Order
                {
                    Id = Guid.NewGuid(),
                    NumberId = Guid.NewGuid(),
                    Address = request.Address,
                    OrderItems = request.Products.Select(dto => new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        ProductId = dto.ProductId,
                        Quantity = dto.Quantity,
                    }).ToList(),
                };

                await _orderRepository.Create(order, ct);
                await _orderRepository.SaveChangesAsync(ct);

                _logger.LogInformation($"Order: {order.Id} created");
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Error create order: {ex.Message}");
            }
        }

        public async Task DeleteOrder(Guid id, CancellationToken ct)
        {
            var existOrder = await _orderRepository.GetItemById(id, ct);

            if (existOrder == null)
            {
                _logger.LogError("Order not found");
            }
            await _orderRepository.Delete(id, ct);
            await _orderRepository.SaveChangesAsync(ct);

            _logger.LogInformation($"Order: {id} deleted");
        }

        public async Task Update(UpdateOrderRequest request, CancellationToken ct)
        {
            try
            {
                var order = new Order
                {
                    Id = request.Id,
                    Address = request.Address,
                    OrderItems = request.Products.Select(dto => new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        ProductId = dto.ProductId,
                        Quantity = dto.Quantity,
                    }).ToList(),
                };

                await _orderRepository.Update(order, ct);

                _logger.LogInformation($"Order Updated {request.Id}");
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Error update order: {ex.Message}");
            }
        }

        public async Task<List<Order?>> GetListOrder(CancellationToken ct)
        {
            return await _orderRepository.GetAll(ct);
        }

        public async Task<Order?> GetOrderById(Guid id, CancellationToken ct)
        {
            return await _orderRepository.GetItemById(id, ct);
        }
    }
}
