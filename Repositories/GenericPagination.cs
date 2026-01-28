using institute.Data;
using institute.DTOs;
using institute.Entities;
using institute.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace institute.Repositories;

public class GenericPagination
{
    private readonly AppDbContext _Context;

    private readonly IUnitOfWork _unitOfWork;

    GenericPagination(AppDbContext Context, IUnitOfWork UnitOfWork)
    {
        _Context = Context;
        _unitOfWork = UnitOfWork;
    }

    
   
       
   
    

    
    
    
    
    
    
    
    
    
    
    // public  Task<List> GetStudentPageAsync(PaginationQueryDto paginationQuery)
    // {
    //     IQueryable <StudentProfile> result = _Context.StudentProfiles.AsQueryable().Include(s => s.User).AsQueryable();
    //     if (!string.IsNullOrEmpty(paginationQuery.SearchFullName))
    //     {
    //         result = result.Where(p => p.User.FullName.Contains(paginationQuery.SearchFullName));
    //     }
    //
    //     if (!string.IsNullOrEmpty(paginationQuery.SearchEmail))
    //     {
    //         result= result.Where(r=>r.User.Email.Contains(paginationQuery.SearchEmail));
    //     }
    //     int Take=paginationQuery.Take;
    //     int Skip= (paginationQuery.CurrentPage-1)*Take;
    //     int CurrentPage=paginationQuery.CurrentPage;
    //     int PageCount = (int)Math.Ceiling((double)result.Count() / Take);
    //     var users=result.OrderBy(u=>u.User.FullName).Skip(Skip).Take(Take)
    //         .ToList();
    //     
    

}


