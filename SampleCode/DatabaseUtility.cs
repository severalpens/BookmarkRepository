using SampleCode.Data;
using Microsoft.EntityFrameworkCore;

public static class DatabaseUtility
{
    /// <summary>
    /// Method to see the database. Should not be used in production: demo purposes only.
    /// </summary>
    /// <param name="options">The configured options.</param>
    /// <param name="count">The number of bookmarks to seed.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    public static async Task EnsureDbCreatedAndSeedWithCountOfAsync(DbContextOptions<BookmarkContext> options, int count)
    {
        // empty to avoid logging while inserting (otherwise will flood console)
        var factory = new LoggerFactory();
        var builder = new DbContextOptionsBuilder<BookmarkContext>(options)
            .UseLoggerFactory(factory);

        using var context = new BookmarkContext(builder.Options);
        // result is true if the database had to be created
        if (await context.Database.EnsureCreatedAsync())
        {
            var seed = new SeedBookmarks();
            await seed.SeedDatabaseWithBookmarkCountOfAsync(context, count);
        }
    }
}
