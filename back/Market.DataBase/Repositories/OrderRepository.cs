using Market.DataBase.Abstractions.Repositories;
using Market.DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Market.DataBase.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext dbContext) 
        {
            _context = dbContext;
        }

        public async Task Create(Order item, CancellationToken ct)
        {
            await _context.Orders.AddAsync(item, ct);
        }

        public async Task Delete(Guid id, CancellationToken ct)
        {
            await _context.Orders
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync(ct);
        }

        public async Task<List<Order>?> GetAll(CancellationToken ct)
        {
            return await _context.Orders
                .Include(x => x.OrderItems)
                    .ThenInclude(x => x.Product)
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<Order?> GetItemById(Guid id, CancellationToken ct)
        {
            return await _context.Orders
                .Include(x => x.OrderItems)
                    .ThenInclude(x => x.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, ct);
                
        }

        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _context.SaveChangesAsync(ct);
        }

        public async Task Update(Order item, CancellationToken ct)
        {
            await _context.OrderItems
                .Where(x => x.OrderId == item.Id)
                .ExecuteDeleteAsync(ct);

            var existing = await _context.Orders
                .FirstOrDefaultAsync(x => x.Id == item.Id, ct);

            if (existing == null)
            {
                throw new InvalidOperationException($"Заказ {item.Id} не найден");
            }

            existing.Address = item.Address;

            foreach (var oi in item.OrderItems)
            {
                oi.OrderId = existing.Id;
                if (oi.Id == Guid.Empty) oi.Id = Guid.NewGuid();  
            }
            _context.OrderItems.AddRange(item.OrderItems);

            await _context.SaveChangesAsync(ct);
        }
    }
}
