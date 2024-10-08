namespace BankSystem.App.Interfaces;

public interface IStorage<T>
{
    public List<T> Get(int pageNumber, int pageSize);
    
    public void Add(T item);
    
    public void Update(T item);

    public void Delete(T item);
}