using BookmarkRepository.Models;
using Microsoft.EntityFrameworkCore;

namespace BookmarkRepository.Data
{
    public class BookmarkService
    {
        BookmarkContext context;
        public BookmarkService(BookmarkContext _context)
        {
            context = _context;
        }

        public async Task<List<Bookmark>> GetBookmarksAsync(string Username)
        {
            return await context.Bookmarks.Where(x => x.Username == Username).ToListAsync();    
        }


        public async Task UploadAsync(List<Bookmark> newBookmarks)
        {
            context.Bookmarks.AddRange(newBookmarks);
            await context.SaveChangesAsync();

        }


        public async Task<Bookmark> GetBookmarkByIdAsync(int id)
        {
            return await context.Bookmarks.FindAsync(id);
        }

        public async Task CreateBookmarkAsync(Bookmark bookmark)
        {
            context.Bookmarks.Add(bookmark);
            await context.SaveChangesAsync();
        }

        public async Task UpdateBookmarkAsync(Bookmark bookmark)
        {
            context.Entry(bookmark).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteBookmarkAsync(int id)
        {
            var bookmark = await context.Bookmarks.FindAsync(id);
            context.Bookmarks.Remove(bookmark);
            await context.SaveChangesAsync();
        }
    }
}
