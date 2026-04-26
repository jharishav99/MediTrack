using Microsoft.EntityFrameworkCore;
using MediTrack.API.Data;
using MediTrack.API.Interfaces;

namespace MediTrack.API.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _context; // here we are using the _context as appdbcontext data 
    private readonly DbSet<T> _dbSet;// here we are using the _dbset as dbset of type T, which is the generic type that we are using in this repository class   
    public Repository(AppDbContext context)
    {
        _context = context; // create constructor and copy _context to context 
        _dbSet = context.Set<T>(); // and _dbSet to context.Set<T>() 
    }
    // now implement the methods of IRepository<T> interface
    public async Task<IEnumerable<T>> GetAllAsync()
     => await _dbSet.ToListAsync(); // here we need all data so we use tolistasync
    
    public async Task<T?> GetByIdAsync(int id)
        => await _dbSet.FindAsync(id);  // here we need to find data by id so we use findasync and return the result

    public async Task<T> CreateAsync(T entity)
    {
        await _dbSet.AddAsync(entity); // here we need to add data so we use addasync and pass the entity as parameter
        await _context.SaveChangesAsync(); // here we need to save changes to the database so we use savechangesasync and return the entity that we just added
        return entity;
    }
    public async Task<T> UpdateAsync(T entity)
    {
        _dbSet.Update(entity); // here we need to update data so we use update and pass the entity as parameter
        await _context.SaveChangesAsync(); // here we need to save changes to the database so we use savechangesasync and return the entity that we just updated
        return entity;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id); // here we need to find data by id so we use findasync and store the result in a variable called entity
        if (entity == null) return false; // here we need to check if the entity is null or not, if it is null then we return false because we cannot delete something that does not exist
        _dbSet.Remove(entity); // here we need to remove the entity from the database so we use remove and pass the entity as parameter
        await _context.SaveChangesAsync(); // here we need to save changes to the database so we use savechangesasync and return true because we have successfully deleted the entity
        return true;
    }
    public async Task<bool> ExistsAsync(int id) // here we need to check if the entity exists or not by id so we use findasync and check if the result is not null, if it is not null then it means that the entity exists and we return true, otherwise we return false

         => await _dbSet.FindAsync(id) != null; 

}