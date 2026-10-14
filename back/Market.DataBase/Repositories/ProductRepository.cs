using Market.DataBase.Abstractions.Repositories;
using Market.DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Market.DataBase.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task Create(Product item, CancellationToken ct)
        {
            await _context.Products.AddAsync(item, ct);
        }

        public async Task Delete(Guid id, CancellationToken ct)
        {
            await _context.Products
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync(ct);
        }

        public async Task<List<Product>?> GetAll(CancellationToken ct)
        {
            return await _context.Products
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<Product?> GetItemById(Guid id, CancellationToken ct)
        {
            return await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _context.SaveChangesAsync(ct);
        }

        public async Task Update(Product item, CancellationToken ct)
        {
            await _context.Products
                .Where(x => x.Id == item.Id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(p => p.Name, item.Name)
                    .SetProperty(p => p.Price, item.Price)
                , ct);
        }
    }
}
