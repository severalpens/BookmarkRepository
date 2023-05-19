using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace SampleCode.Data;

public class BookmarkContext : DbContext
{
    public static readonly string RowVersion = nameof(RowVersion);

    public static readonly string BookmarksDb = nameof(BookmarksDb).ToLower();

    public BookmarkContext(DbContextOptions<BookmarkContext> options)
        : base(options)
    {
        Debug.WriteLine($"{ContextId} context created.");
    }

    public DbSet<Bookmark>? Bookmarks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // this property isn't on the C# class
        // so we set it up as a "shadow" property and use it for concurrency
        modelBuilder.Entity<Bookmark>()
            .Property<byte[]>(RowVersion)
            .IsRowVersion();

        base.OnModelCreating(modelBuilder);
    }

    public override void Dispose()
    {
        Debug.WriteLine($"{ContextId} context disposed.");
        base.Dispose();
    }

    public override ValueTask DisposeAsync()
    {
        Debug.WriteLine($"{ContextId} context disposed async.");
        return base.DisposeAsync();
    }
}
