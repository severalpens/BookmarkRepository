namespace SampleCode.Grid;

/// <summary>
/// State of grid filters.
/// </summary>
public class GridControls : IBookmarkFilters
{
    /// <summary>
    /// Keep state of paging.
    /// </summary>
    public IPageHelper PageHelper { get; set; }

    public GridControls(IPageHelper pageHelper)
    {
        PageHelper = pageHelper;
    }

    /// <summary>
    /// Avoid multiple concurrent requests.
    /// </summary>
    public bool Loading { get; set; }


    /// <summary>
    /// Column to sort by.
    /// </summary>
    public BookmarkFilterColumns SortColumn { get; set; }
        = BookmarkFilterColumns.Name;

    /// <summary>
    /// True when sorting ascending, otherwise sort descending.
    /// </summary>
    public bool SortAscending { get; set; } = true;

    /// <summary>
    /// Column filtered text is against.
    /// </summary>
    public BookmarkFilterColumns FilterColumn { get; set; }
        = BookmarkFilterColumns.Name;

    /// <summary>
    /// Text to filter on.
    /// </summary>
    public string? FilterText { get; set; }
}
