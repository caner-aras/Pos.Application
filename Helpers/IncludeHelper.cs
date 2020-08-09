using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class IncludeHelper
{
    internal static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query,
     params string[] includes) where T : class
    {
        if (includes != null)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }
        return query;
    }
}
