using Task3.Entities;
using Task3.Models;

namespace Task3.Repository.Abstract;

public interface IBookRepository
{
    Task<List<Book>> Get();
    Task<Book?> Get(int id);
    Task<Book> Add(Book book);
    Task<Book> Update(Book book);
    Task Delete(Book book);
    Task<bool> SaveChanges();
    Task<PagedResult<Book>> GetAll(int page, int pageSize);
}
