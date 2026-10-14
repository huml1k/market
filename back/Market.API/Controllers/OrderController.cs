using Market.Contracts.Requests.Orders;
using Market.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Market.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("create/order")]
        public async Task<IResult> CreateOrder(CreateOrderRequest request, CancellationToken ct)
        {
            try
            {
                await _orderService.CreateOrder(request, ct);

                return TypedResults.Ok($"Заказ создан");
            }
            catch (Exception ex)
            {
                return TypedResults.Conflict($"Ошибка на стороне сервера: {ex.Message}");
            }
        }

        [HttpDelete("order/{id}")]
        public async Task<IResult> DeleteOrder(Guid id, CancellationToken ct)
        {
            try
            {
                await _orderService.DeleteOrder(id, ct);

                return TypedResults.Ok($"Заказ под {id} удален");
            }
            catch (Exception ex)
            {
                return TypedResults.Conflict($"Ошибка на стороне сервера: {ex.Message}");
            }
        }

        [HttpPost("update/order")]
        public async Task<IResult> UpdateOrder(UpdateOrderRequest request, CancellationToken ct)
        {
            try
            {
                await _orderService.Update(request, ct);

                return TypedResults.Ok($"Заказ: {request} изменен");
            }
            catch (Exception ex)
            {
                return TypedResults.Conflict($"Ошибка на стороне сервера: {ex.Message}");
            }
        }

        [HttpGet("list/order")]
        public async Task<IResult> GetOrders(CancellationToken ct)
        {
            try
            {
                var result = await _orderService.GetListOrder(ct);

                return TypedResults.Ok(result);
            }
            catch (Exception ex)
            {
                return TypedResults.Conflict($"Ошибка на стороне сервера: {ex.Message}");
            }
        }

        [HttpGet("order/{id}")]
        public async Task<IResult> GetOrderById(Guid id, CancellationToken ct)
        {
            try
            {
                var result = await _orderService.GetOrderById(id, ct);

                return TypedResults.Ok(result);
            }
            catch (Exception ex)
            {
                return TypedResults.Conflict($"Ошибка на стороне сервера: {ex.Message}");
            }
        }
    }
}
