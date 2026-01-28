using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace institute.Helpers;

public static class ExpressionHelper
{
    public static Expression<Func<T, object>> BuildOrderBy<T>(string? sortBy)
    {

        if (string.IsNullOrWhiteSpace(sortBy))
            return x => EF.Property<object>(x, "Id");

        return x => EF.Property<object>(x, sortBy);
    }
}