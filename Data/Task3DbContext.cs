using Microsoft.EntityFrameworkCore;
using Task3.Entities;

namespace Task3.Data;

public class Task3DbContext : DbContext
{
    public Task3DbContext(DbContextOptions<Task3DbContext> options) : base(options) { }

    public DbSet<Book> Books => Set<Book>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasData(
            new Book { Id = 1, Title = "Clean Code", Author = "Robert C. Martin", Category = "Programming", Price = 45, Stock = 10, PageCount = 464, PublishedYear = 2008, IsAvailable = true },
            new Book { Id = 2, Title = "Atomic Habits", Author = "James Clear", Category = "Self Development", Price = 30, Stock = 8, PageCount = 320, PublishedYear = 2018, IsAvailable = true },
            new Book { Id = 3, Title = "1984", Author = "George Orwell", Category = "Novel", Price = 20, Stock = 5, PageCount = 328, PublishedYear = 1949, IsAvailable = true },
            new Book { Id = 4, Title = "The Hobbit", Author = "J.R.R. Tolkien", Category = "Fantasy", Price = 35, Stock = 7, PageCount = 310, PublishedYear = 1937, IsAvailable = true },
            new Book { Id = 5, Title = "Rich Dad Poor Dad", Author = "Robert Kiyosaki", Category = "Finance", Price = 25, Stock = 0, PageCount = 336, PublishedYear = 1997, IsAvailable = false },
            new Book { Id = 6, Title = "The Pragmatic Programmer", Author = "Andrew Hunt", Category = "Programming", Price = 50, Stock = 6, PageCount = 352, PublishedYear = 1999, IsAvailable = true }
        );
    }
}
