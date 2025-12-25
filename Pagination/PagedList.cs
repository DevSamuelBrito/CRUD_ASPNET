using System;

namespace CRUD_ASPNET.Pagination;

public class PagedList<T> : List<T> where T : class
{
    public int CurrentPage { get; private set; } = 1; 
    public int TotalPages { get; private set; }
    public int PageSize { get; private set; } = 20;
    public int TotalCount { get; private set; } 
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    //construtor
    public PagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        AddRange(items);
    }

    //metodo static
    public static PagedList<T> ToPagedList(IQueryable<T> source,
    int pageNumber, int pageSize)
    {
        var count = source.Count();
        var items = source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}
