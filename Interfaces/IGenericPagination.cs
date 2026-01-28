using institute.DTOs;
using institute.Entities;

namespace institute.Interfaces;

public interface IGenericPagination<T> where T :class
{
     PageResultStudentDto GetStudentPageAsync(PaginationQueryDto paginationQuery);
}