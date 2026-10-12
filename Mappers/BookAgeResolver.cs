using AutoMapper;
using Task3.Dtos;
using Task3.Entities;
using Task3.Services.Abstract;

namespace Task3.Mappers;

public class BookAgeResolver : IValueResolver<Book, BookExtendDto, int>
{
    private readonly IBookService _bookService;

    public BookAgeResolver(IBookService bookService) => _bookService = bookService;

    public int Resolve(Book source, BookExtendDto destination, int destMember, ResolutionContext context)
        => _bookService.GetBookAge(source);
}
