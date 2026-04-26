using Microsoft.EntityFrameworkCore;
using MediTrack.API.Data;
using MediTrack.API.Interfaces;

namespace MediTrack.API.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _context; // AppDbContext ko instance lai _context variable ma store garna, jasko madhyam bata database operations perform garna sakincha
    private readonly DbSet<T> _dbSet; // DbSet<T> ko instance lai _dbSet variable ma store garna, jasko madhyam bata specific entity type T ko database operations perform garna sakincha

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
     => await _dbSet.ToListAsync(); // DbSet<T> ko ToListAsync() method lai call garna, jasko madhyam bata database ma bhayeko sabai records lai list ma convert garna sakincha

    public async Task<T?> GetByIdAsync(int id)
        => await _dbSet.FindAsync(id); // DbSet<T> ko FindAsync() method lai call garna, jasko madhyam bata database ma bhayeko specific record lai id ko basis ma find garna sakincha

    public async Task<T> CreateAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    public async Task<T> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null) return false;
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> ExistsAsync(int id)

         => await _dbSet.FindAsync(id) != null;

}