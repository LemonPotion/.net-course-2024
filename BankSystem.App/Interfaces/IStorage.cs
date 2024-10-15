namespace BankSystem.App.Interfaces;

public interface IStorage<T>
{
    public T GetById(Guid id);
    public List<T> Get(int pageNumber, int pageSize, Func<T, bool>? filter);
    
    public void Add(T item);
    
    public void Update(T employee);

    public void Delete(Guid id);
}