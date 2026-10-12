using Task3.Entities;
using Task3.Models;
using Task3.Repository.Abstract;
using Task3.Services.Abstract;

namespace Task3.Services.Concrete;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;

    public BookService(IBookRepository repository) => _repository = repository;

    public async Task<List<Book>> Get() => await _repository.Get();
    public async Task<Book?> Get(int id) => await _repository.Get(id);
    public async Task<PagedResult<Book>> GetAll(int page, int pageSize) => await _repository.GetAll(page, pageSize);

    public async Task<Book> Add(Book book)
    {
        var result = await _repository.Add(book);
        await _repository.SaveChanges();
        return result;
    }

    public async Task<Book> Update(Book book)
    {
        var result = await _repository.Update(book);
        await _repository.SaveChanges();
        return result;
    }

    public async Task<bool> Delete(Book book)
    {
        await _repository.Delete(book);
        return await _repository.SaveChanges();
    }

    public int GetBookAge(Book book)
    {
        var age = DateTime.Now.Year - book.PublishedYear;
        return age > 0 ? age : 0;
    }
}
