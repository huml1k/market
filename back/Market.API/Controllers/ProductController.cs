using Market.Contracts.Requests.Products;
using Market.Services.Abstractions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Market.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService) 
        {
            _productService = productService;
        }

        [HttpPost("create/product")]
        public async Task<IResult> CreateProduct(CreateProductRequest createProduct, CancellationToken ct) 
        {
            try
            {
                await _productService.CreateProduct(createProduct, ct);

                return TypedResults.Ok($"Продукт создан {createProduct.Name}");
            }
            catch (Exception ex)
            {
                return TypedResults.Conflict($"Ошибка на стороне сервера: {ex.Message}");
            }
        }

        [HttpDelete("product/{id}")]
        public async Task<IResult> DeleteProduct(Guid id, CancellationToken ct) 
        {
            try 
            {
                await _productService.DeleteProduct(id, ct);

                return TypedResults.Ok($"Продукт под id:{id} успешно удален");
            }
            catch(Exception ex)
            {
                return TypedResults.Conflict($"Ошибка на стороне сервера: {ex.Message}");
            }
        }

        [HttpPost("update/product")]
        public async Task<IResult> UpdateProduct(UpdateProductRequest request, CancellationToken ct) 
        {
            try 
            {
                await _productService.Update(request, ct);

                return TypedResults.Ok($"Продукт {request} изменен");
            } 
            catch (Exception ex)
            {
                return TypedResults.Conflict($"Ошибка на стороне сервера: {ex.Message}");
            }
        }

        [HttpGet("list/product")]
        public async Task<IResult> GetProducts(CancellationToken ct) 
        {
            try
            {
                var result = await _productService.GetListProducts(ct);

                return TypedResults.Ok(result);
            }
            catch (Exception ex)
            {
                return TypedResults.Conflict($"Ошибка на стороне сервера: {ex.Message}");
            }
        }

        [HttpGet("product/{id}")]
        public async Task<IResult> GetProductById(Guid id, CancellationToken ct) 
        {
            try
            {
                var result = await _productService.GetProductById(id, ct);

                return TypedResults.Ok(result);
            }
            catch (Exception ex)
            {
                return TypedResults.Conflict($"Ошибка на стороне сервера: {ex.Message}");
            }
        }
    }
}
