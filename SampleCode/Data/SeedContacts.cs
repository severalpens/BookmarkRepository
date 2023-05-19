namespace SampleCode.Data;

/// <summary>
/// Generates desired number of random bookmarks.
/// </summary>
public class SeedBookmarks
{
    /// <summary>
    /// Use these to make names.
    /// </summary>
    private readonly string[] _gems = new[] {
        "Diamond",
        "Crystal",
        "Morion",
        "Azore",
        "Sapphire",
        "Cobalt",
        "Aquamarine",
        "Montana",
        "Turquoise",
        "Lime",
        "Erinite",
        "Emerald",
        "Turmaline",
        "Jonquil",
        "Olivine",
        "Topaz",
        "Citrine",
        "Sun",
        "Quartz",
        "Opal",
        "Alabaster",
        "Rose",
        "Burgundy",
        "Siam",
        "Ruby",
        "Amethyst",
        "Violet",
        "Lilac"};

    /// <summary>
    /// Combined with things for last names.
    /// </summary>
    private readonly string[] _colors = new[]
    {
        "Blue",
        "Aqua",
        "Red",
        "Green",
        "Orange",
        "Yellow",
        "Black",
        "Violet",
        "Brown",
        "Crimson",
        "Gray",
        "Cyan",
        "Magenta",
        "White",
        "Gold",
        "Pink",
        "Lavender"
    };

    /// <summary>
    /// Also helpful for names.
    /// </summary>
    private readonly string[] _things = new[]
    {
        "beard",
        "finger",
        "hand",
        "toe",
        "stalk",
        "hair",
        "vine",
        "url",
        "son",
        "brook",
        "river",
        "lake",
        "stone",
        "ship"
    };

    /// <summary>
    /// Url names.
    /// </summary>
    private readonly string[] _urls = new[]
    {
        "Broad",
        "Wide",
        "Main",
        "Pine",
        "Ash",
        "Poplar",
        "First",
        "Third",
    };

    /// <summary>
    /// Types of urls.
    /// </summary>
    private readonly string[] _urlTypes = new[]
    {
        "Url",
        "Lane",
        "Place",
        "Terrace",
        "Drive",
        "Way"
    };

    /// <summary>
    /// More uniqueness.
    /// </summary>
    private readonly string[] _directions = new[]
    {
        "N",
        "NE",
        "E",
        "SE",
        "S",
        "SW",
        "W",
        "NW"
    };

    /// <summary>
    /// A sampling of cities.
    /// </summary>
    private readonly string[] _cities = new[]
    {
        "Austin",
        "Denver",
        "Fayetteville",
        "Des Moines",
        "San Francisco",
        "Portland",
        "Monroe",
        "Redmond",
        "Bothel",
        "Woodinville",
        "Kent",
        "Kennesaw",
        "Marietta",
        "Atlanta",
        "Lead",
        "Spokane",
        "Bellevue",
        "Seattle"
    };

    /// <summary>
    /// State list.
    /// </summary>
    private readonly string[] _states = new[]
    {
        "AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "FL",
        "GA", "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA",
        "ME", "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE",
        "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "OH", "OK",
        "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VT",
        "VA", "WA", "WV", "WI", "WY"
    };

    /// <summary>
    /// Get some randominzation in play.
    /// </summary>
    private readonly Random _random = new();

    /// <summary>
    /// Picks a random item from a list.
    /// </summary>
    /// <param name="list">A list of <c>string</c> to parse.</param>
    /// <returns>A single item from the list.</returns>
    private string RandomOne(string[] list)
    {
        var idx = _random.Next(list.Length - 1);
        return list[idx];
    }

    /// <summary>
    /// Make a new bookmark.
    /// </summary>
    /// <returns>A random <see cref="Bookmark"/> instance.</returns>
    private Bookmark MakeBookmark()
    {
        var bookmark = new Bookmark
        {
            Name = RandomOne(_gems),
            Url = RandomOne(_colors)
        };
        return bookmark;
    }

    public async Task SeedDatabaseWithBookmarkCountOfAsync(BookmarkContext context, int totalCount)
    {
        var count = 0;
        var currentCycle = 0;
        while (count < totalCount)
        {
            var list = new List<Bookmark>();
            while (currentCycle++ < 100 && count++ < totalCount)
            {
                list.Add(MakeBookmark());
            }
            if (list.Count > 0)
            {
                context.Bookmarks?.AddRange(list);
                await context.SaveChangesAsync();
            }
            currentCycle = 0;
        }
    }
}
