﻿using Microsoft.EntityFrameworkCore;
using WorkHive.Data.Models;

namespace WorkHive.Data.Base;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected WorkHiveContext _context;

    public GenericRepository()
    {
        _context ??= new WorkHiveContext();
    }


    public GenericRepository(WorkHiveContext context)
    {
        _context = context;
    }
    #region Separating asign entity and save operators
    public void PrepareCreate(T entity)
    {
        _context.Add(entity);
    }

    public void PrepareUpdate(T entity)
    {
        var tracker = _context.Attach(entity);
        tracker.State = EntityState.Modified;
    }

    public void PrepareRemove(T entity)
    {
        _context.Remove(entity);
    }

    #endregion Separating asign entity and save operators


    public List<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }
    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }
    public void Create(T entity)
    {
        _context.Add(entity);
        _context.SaveChanges();
    }

    public async Task<int> CreateAsync(T entity)
    {
        
        //try
        //{
            _context.Add(entity);
            return await _context.SaveChangesAsync();
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine("Lỗi SaveChangesAsync: " + ex.Message);
        //    if (ex.InnerException != null)
        //        Console.WriteLine("Inner: " + ex.InnerException.Message);
        //    throw;
        //}

    }

    public void Update(T entity)
    {
        var tracker = _context.Attach(entity);
        tracker.State = EntityState.Modified;
        _context.SaveChanges();
    }

    public async Task<int> UpdateAsync(T entity)
    {
        var tracker = _context.Attach(entity);
        tracker.State = EntityState.Modified;

        return await _context.SaveChangesAsync();
    }

    public bool Remove(T entity)
    {
        _context.Remove(entity);
        _context.SaveChanges();
        return true;
    }

    public async Task<bool> RemoveAsync(T entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public T GetById(int id)
    {
        return _context.Set<T>().Find(id);
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public T GetById(string code)
    {
        return _context.Set<T>().Find(code);
    }

    public async Task<T> GetByIdAsync(string code)
    {
        return await _context.Set<T>().FindAsync(code);
    }

    public T GetById(Guid code)
    {
        return _context.Set<T>().Find(code);
    }

    public async Task<T> GetByIdAsync(Guid code)
    {
        return await _context.Set<T>().FindAsync(code);
    }
}
