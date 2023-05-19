using BookmarkRepository.Models;
using Microsoft.EntityFrameworkCore;
using HtmlAgilityPack;

namespace BookmarkRepository.Data
{
    public class BookmarkHtmlParser
    {
        public List<Bookmark> ParseBookmarks(Stream? stream)
        {
            List<Bookmark> bookmarks = new List<Bookmark>();

            if (stream != null)
            {
                // Load the HTML document from the stream using HtmlAgilityPack
                HtmlDocument doc = new HtmlDocument();
                doc.Load(stream);

                // Find all the <a> elements that contain bookmarks
                IEnumerable<HtmlNode> nodes = doc.DocumentNode.Descendants("a")
                    .Where(n => n.Attributes["href"] != null && n.Attributes["href"].Value.StartsWith("http"));

                // Create a bookmark for each <a> element and add it to the list
                foreach (HtmlNode node in nodes)
                {
                    Bookmark bookmark = new Bookmark()
                    {
                        Name = node.InnerHtml.Trim(),
                        Url = node.Attributes["href"].Value
                    };

                    bookmarks.Add(bookmark);
                }
            }

            return bookmarks;
        }
    }
}
