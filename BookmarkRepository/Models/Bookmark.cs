using System.ComponentModel.DataAnnotations.Schema;

namespace BookmarkRepository.Models
{
    [Table("bookmarks", Schema = "bl")]
    public class Bookmark
    {
        public int BookmarkId { get; set; }

        public string? Username { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
