using IsuReservation.Abstract;
using IsuReservation.Models;
using Microsoft.EntityFrameworkCore;

namespace IsuReservation.Data;

public class EfRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _dbContext;

    public EfRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T?> GetById(Guid id)
    {
        return await _dbContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
    }

    public IQueryable<T> List()
    {
        return _dbContext.Set<T>().AsQueryable();
    }

    public async Task<List<T>> ListAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<T> Add(T entity)
    {
        entity.DateCreated = DateTime.Now;
        _dbContext.Set<T>().Add(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(T entity)
    {
        entity.DateModified = DateTime.Now;
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }
}