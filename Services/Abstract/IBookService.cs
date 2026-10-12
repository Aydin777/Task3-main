using Task3.Entities;
using Task3.Models;

namespace Task3.Services.Abstract;

public interface IBookService
{
    Task<List<Book>> Get();
    Task<Book?> Get(int id);
    Task<Book> Add(Book book);
    Task<Book> Update(Book book);
    Task<bool> Delete(Book book);
    Task<PagedResult<Book>> GetAll(int page, int pageSize);
    int GetBookAge(Book book);
}
