using Market.Contracts.Requests.Products;
using Market.DataBase.Abstractions.Repositories;
using Market.DataBase.Entities;
using Market.Services.Abstractions;
using Microsoft.Extensions.Logging;

namespace Market.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(
            IRepository<Product> productRepository,
            ILogger<ProductService> logger)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task CreateProduct(CreateProductRequest request, CancellationToken ct)
        {
            try
            {
                var product = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Price = request.Price,
                };

                await _productRepository.Create(product, ct);
                await _productRepository.SaveChangesAsync(ct);

                _logger.LogInformation($"Product created: {product.Id}");
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Error create product: {ex.Message}");
            }
        }

        public async Task DeleteProduct(Guid id, CancellationToken ct)
        {
            var existOrder = await _productRepository.GetItemById(id, ct);

            if (existOrder == null)
            {
                _logger.LogError("Order not found");
            }

            await _productRepository.Delete(id, ct);
            await _productRepository.SaveChangesAsync(ct);

            _logger.LogInformation($"Product Deleted {id}");
        }

        public async Task<List<Product?>> GetListProducts(CancellationToken ct)
        {
            return await _productRepository.GetAll(ct);
        }

        public async Task<Product?> GetProductById(Guid id, CancellationToken ct)
        {
            return await _productRepository.GetItemById(id, ct);
        }

        public async Task Update(UpdateProductRequest request, CancellationToken ct)
        {
            try
            {
                var existOrder = await _productRepository.GetItemById(request.Id, ct);

                if (existOrder == null)
                {
                    _logger.LogError("Order not found");
                }

                var product = new Product
                {
                    Id = request.Id,
                    Name = request.Name,
                    Price = request.Price,
                };

                await _productRepository.Update(product, ct);

                _logger.LogInformation($"Product Updated: {request.Id}");
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Error update product: {ex.Message}");
            }
        }
    }
}
