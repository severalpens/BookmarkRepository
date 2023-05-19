namespace SampleCode.Grid;

/// <summary>
/// Interface for filtering.
/// </summary>
public interface IBookmarkFilters
{
    /// <summary>
    /// The <see cref="BookmarkFilterColumns"/> being filtered on.
    /// </summary>
    BookmarkFilterColumns FilterColumn { get; set; }

    /// <summary>
    /// Loading indicator.
    /// </summary>
    bool Loading { get; set; }

    /// <summary>
    /// The text of the filter.
    /// </summary>
    string? FilterText { get; set; }

    /// <summary>
    /// Paging state in <see cref="PageHelper"/>.
    /// </summary>
    IPageHelper PageHelper { get; set; }

    /// <summary>
    /// Gets or sets a value indicating if the sort is ascending or descending.
    /// </summary>
    bool SortAscending { get; set; }

    /// <summary>
    /// The <see cref="BookmarkFilterColumns"/> being sorted.
    /// </summary>
    BookmarkFilterColumns SortColumn { get; set; }
}
