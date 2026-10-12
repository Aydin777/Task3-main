using Microsoft.EntityFrameworkCore;
using Task3.Data;
using Task3.Entities;
using Task3.Models;
using Task3.Repository.Abstract;

namespace Task3.Repository.Concrete;

public class BookRepository : IBookRepository
{
    private readonly Task3DbContext _context;

    public BookRepository(Task3DbContext context) => _context = context;

    public async Task<List<Book>> Get() => await _context.Books.AsNoTracking().ToListAsync();

    public async Task<Book?> Get(int id) => await _context.Books.FirstOrDefaultAsync(b => b.Id == id);

    public async Task<Book> Add(Book book) => (await _context.Books.AddAsync(book)).Entity;

    public Task<Book> Update(Book book) => Task.FromResult(_context.Books.Update(book).Entity);

    public Task Delete(Book book)
    {
        _context.Books.Remove(book);
        return Task.CompletedTask;
    }

    public async Task<bool> SaveChanges() => await _context.SaveChangesAsync() > 0;

    public async Task<PagedResult<Book>> GetAll(int page, int pageSize)
    {
        var query = _context.Books.AsNoTracking().OrderBy(b => b.Id);
        var totalCount = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PagedResult<Book>
        {
            items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }
}
