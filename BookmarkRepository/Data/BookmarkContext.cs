using BookmarkRepository.Models;
using Microsoft.EntityFrameworkCore;

namespace  BookmarkRepository.Data
{
    public partial class BookmarkContext : DbContext
    {
        public DbSet<Bookmark> Bookmarks { get; set; }

        public BookmarkContext()
        {

        }


        public BookmarkContext(DbContextOptions<BookmarkContext> options)
            : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.UseSqlServer("DefaultConnection");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


           OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
