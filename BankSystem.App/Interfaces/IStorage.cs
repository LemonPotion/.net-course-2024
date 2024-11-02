using System.Linq.Expressions;

namespace BankSystem.App.Interfaces;

public interface IStorage<T>
{
    public Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<List<T>> GetAsync(int pageNumber, int pageSize, Expression<Func<T, bool>>? filter, CancellationToken cancellationToken);

    public Task AddAsync(T item, CancellationToken cancellationToken);

    public Task UpdateAsync(Guid id, T item, CancellationToken cancellationToken);

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}