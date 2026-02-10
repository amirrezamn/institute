using System.Linq.Expressions;
using institute.Data;
using institute.DTOs;
using institute.Entities;
using institute.Interfaces;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Microsoft.EntityFrameworkCore;

namespace institute.Repositories;

public class GenericRepository<T>: IRepository<T> where T : class
{
    private readonly AppDbContext _Context;
    private readonly DbSet<T> _dbSet;
    public GenericRepository(AppDbContext Context)
    {
        _Context = Context;
        _dbSet = Context.Set<T>();
    }
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var entities = await _dbSet.ToListAsync();
        return entities;
    }

    // public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
    // {
    //     return await _dbSet.Where(predicate).ToListAsync();
    // }
    
    public async Task<List<T>> FindAsync(
        Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.Where(predicate).ToListAsync();
    }


    public async Task<PagedResult<T>> Pagination<TKey>(int pageNumber, int pageSize,
        Expression<Func<T,TKey>> orderBy,bool ascending=true,Expression<Func<T, bool>>? filter = null,params Expression<Func<T, object>>[] includes)
        
    {
        IQueryable <T> result = _dbSet.AsQueryable();
        if (pageNumber<1)
        {
            throw  new ArgumentOutOfRangeException((nameof(pageNumber)),"pageNumber must be greater than or equal to 1.");
        }

        if (pageSize < 1)
        {
            throw new ArgumentOutOfRangeException((nameof(pageSize)),"pageSize must be greater than or equal to 1.");
        }

        if (orderBy==null)
        {
            throw new ArgumentNullException(nameof(orderBy),"orderBy must be supplied.");
        }

        // if (!string.IsNullOrEmpty(filterFieldName))
        // {
        //     var parameter = Expression.Parameter(typeof(T), "x");
        //     var property = Expression.Property(parameter, filterFieldName);
        //     var constant = Expression.Constant(filterValue);
        //     var equal = Expression.Equal(property, constant);
        //     var lambda = Expression.Lambda<Func<T, bool>>(equal, parameter);
        //     result= result.Where(lambda);
        //
        // }
        if (typeof(ISoftDelete).IsAssignableFrom(typeof(T)))
        {
            result=result.Where(e=>EF.Property<bool>(e,"IsDeleted"));
        }

        foreach (var include in includes)
        {
            result = result.Include(include);
        }

        if (filter != null)
        {
            result = result.Where(filter);
        }
        var totalCount = await result.CountAsync();
        
        var items = await result.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        
        result = ascending?result.OrderBy(orderBy):result.OrderByDescending(orderBy);
       
        int skip= (pageNumber-1)*pageSize;

        return new PagedResult<T>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

    }
    // var list=result.Skip(skip).Take(pageSize)
    //     .ToList();

 
    public  async Task<T?> GetByIdAsync(int id)
    {
      var entity = await _dbSet.FindAsync(id);
      return entity;
    }

    public async Task AddAsync(T entity)
    {
       await _dbSet.AddAsync(entity);  
    }

    public Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }
    

    public async Task SoftDeleteAsync(T entity)
    {
        if (entity is ISoftDelete soft)
        {
            soft.IsDeleted = true;
            _dbSet.Update(entity);
            await Task.CompletedTask;
        }
    }

    public async Task HardDeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await Task.CompletedTask;
        
    }
    public async Task<IEnumerable<T>> FindIgnoreQueryFiltersAsync(Expression<Func<T,bool>> predicate)
    {
        return await _dbSet.IgnoreQueryFilters().Where(predicate).ToListAsync();
    }
    public async Task<T?> FindOneAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate);
    }

    public async Task<List<T>> FindWithIncludesAsync(
        Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        foreach (var include in includes)
            query = query.Include(include);

        return await query.Where(predicate).ToListAsync();
    }
    public async Task<T?> FindOneWithIncludesAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.FirstOrDefaultAsync(predicate);
    }
}