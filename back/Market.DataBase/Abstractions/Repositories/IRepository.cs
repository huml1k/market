namespace Market.DataBase.Abstractions.Repositories
{
    public interface IRepository<T> where T : class
    {
        public Task Create(T item, CancellationToken ct);
        public Task Update(T item, CancellationToken ct);
        public Task Delete(Guid id, CancellationToken ct);
        public Task SaveChangesAsync(CancellationToken ct);
        public Task<List<T>?> GetAll(CancellationToken ct);
        public Task<T?> GetItemById(Guid id, CancellationToken ct);
    }
}
