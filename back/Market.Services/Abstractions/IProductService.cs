using Market.Contracts.Requests.Products;
using Market.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Market.Services.Abstractions
{
    public interface IProductService
    {
        public Task CreateProduct(CreateProductRequest request, CancellationToken ct);
        public Task DeleteProduct(Guid id, CancellationToken ct);
        public Task Update(UpdateProductRequest request, CancellationToken ct);
        public Task<List<Product?>> GetListProducts(CancellationToken ct);
        public Task<Product?> GetProductById(Guid id, CancellationToken ct);
    }
}
