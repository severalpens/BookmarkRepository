using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SampleCode.Data;

namespace SampleCode.Grid;

/// <summary>
/// Creates the right expressions to filter and sort.
/// </summary>
public class GridQueryAdapter
{
    /// <summary>
    /// Holds state of the grid.
    /// </summary>
    private readonly IBookmarkFilters _controls;

    /// <summary>
    /// Expressions for sorting.
    /// </summary>
    private readonly Dictionary<BookmarkFilterColumns, Expression<Func<Bookmark, string>>> _expressions
        = new()
        {
            { BookmarkFilterColumns.Name, c => c != null && c.Name != null ? c.Name : string.Empty },
            { BookmarkFilterColumns.Url, c => c != null && c.Url != null ? c.Url : string.Empty },
        };

    /// <summary>
    /// Queryables for filtering.
    /// </summary>
    private readonly Dictionary<BookmarkFilterColumns, Func<IQueryable<Bookmark>, IQueryable<Bookmark>>> _filterQueries = 
        new Dictionary<BookmarkFilterColumns, Func<IQueryable<Bookmark>, IQueryable<Bookmark>>>();

    /// <summary>
    /// Creates a new instance of the <see cref="GridQueryAdapter"/> class.
    /// </summary>
    /// <param name="controls">The <see cref="IBookmarkFilters"/> to use.</param>
    public GridQueryAdapter(IBookmarkFilters controls)
    {
        _controls = controls;

        // set up queries
        _filterQueries = new()
        {
            { BookmarkFilterColumns.Name, cs => cs.Where(c => c != null && c.Name != null && _controls.FilterText != null ? c.Name.Contains(_controls.FilterText) : false ) },
            { BookmarkFilterColumns.Url, cs => cs.Where(c => c != null && c.Url != null && _controls.FilterText != null ? c.Url.Contains(_controls.FilterText) : false ) },
        };
    }

    /// <summary>
    /// Uses the query to return a count and a page.
    /// </summary>
    /// <param name="query">The <see cref="IQueryable{Bookmark}"/> to work from.</param>
    /// <returns>The resulting <see cref="ICollection{Bookmark}"/>.</returns>
    public async Task<ICollection<Bookmark>> FetchAsync(IQueryable<Bookmark> query)
    {
        query = FilterAndQuery(query);
        await CountAsync(query);
        var collection = await FetchPageQuery(query)
            .ToListAsync();
        _controls.PageHelper.PageItems = collection.Count;
        return collection;
    }

    /// <summary>
    /// Get total filtered items count.
    /// </summary>
    /// <param name="query">The <see cref="IQueryable{Bookmark}"/> to use.</param>
    /// <returns>Asynchronous <see cref="Task"/>.</returns>
    public async Task CountAsync(IQueryable<Bookmark> query)
    {
        _controls.PageHelper.TotalItemCount = await query.CountAsync();
    }

    /// <summary>
    /// Build the query to bring back a single page.
    /// </summary>
    /// <param name="query">The <see cref="IQueryable{Bookmark}"/> to modify.</param>
    /// <returns>The new <see cref="IQueryable{Bookmark}"/> for a page.</returns>
    public IQueryable<Bookmark> FetchPageQuery(IQueryable<Bookmark> query)
    {
        return query
            .Skip(_controls.PageHelper.Skip)
            .Take(_controls.PageHelper.PageSize)
            .AsNoTracking();
    }

    /// <summary>
    /// Builds the query.
    /// </summary>
    /// <param name="root">The <see cref="IQueryable{Bookmark}"/> to start with.</param>
    /// <returns>
    /// The resulting <see cref="IQueryable{Bookmark}"/> with sorts and
    /// filters applied.
    /// </returns>
    private IQueryable<Bookmark> FilterAndQuery(IQueryable<Bookmark> root)
    {
        var sb = new System.Text.StringBuilder();

        // apply a filter?
        if (!string.IsNullOrWhiteSpace(_controls.FilterText))
        {
            var filter = _filterQueries[_controls.FilterColumn];
            sb.Append($"Filter: '{_controls.FilterColumn}' ");
            root = filter(root);
        }

        // apply the expression
        var expression = _expressions[_controls.SortColumn];
        sb.Append($"Sort: '{_controls.SortColumn}' ");


        var sortDir = _controls.SortAscending ? "ASC" : "DESC";
        sb.Append(sortDir);

        Debug.WriteLine(sb.ToString());
        // return the unfiltered query for total count, and the filtered for fetching
        return _controls.SortAscending ? root.OrderBy(expression)
            : root.OrderByDescending(expression);
    }
}
