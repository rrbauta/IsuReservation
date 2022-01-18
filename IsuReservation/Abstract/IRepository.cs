using IsuReservation.Models;

namespace IsuReservation.Abstract;

public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetById(Guid id);
    IQueryable<T> List();
    Task<List<T>> ListAsync();
    Task<T> Add(T entity);
    Task Update(T entity);
    Task Delete(T entity);
}