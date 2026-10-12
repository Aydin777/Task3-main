using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Task3.Dtos;
using Task3.Entities;
using Task3.Models;
using Task3.Services.Abstract;

namespace Task3.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly IMapper _mapper;

    public BooksController(IBookService bookService, IMapper mapper)
    {
        _bookService = bookService;
        _mapper = mapper;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetAll()
    {
        var books = await _bookService.Get();
        return Ok(_mapper.Map<IEnumerable<BookDto>>(books));
    }


    [HttpGet("partial")]
    public async Task<ActionResult<PagedResult<BookDto>>> GetPartial(int page = 1, int pageSize = 10)
    {
        if (page < 1 || pageSize < 1 || pageSize > 100)
            return BadRequest("page 1-dən, pageSize isə 1-dən 100-ə qədər olmalıdır.");

        var result = await _bookService.GetAll(page, pageSize);
        return Ok(new PagedResult<BookDto>
        {
            items = _mapper.Map<List<BookDto>>(result.items),
            Page = result.Page,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount,
            TotalPages = result.TotalPages
        });
    }

    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<BookDto>> GetById(int id)
    {
        var book = await _bookService.Get(id);
        return book == null ? NotFound() : Ok(_mapper.Map<BookDto>(book));
    }

    
    [HttpGet("{id:int}/extend")]
    public async Task<ActionResult<BookExtendDto>> GetExtend(int id)
    {
        var book = await _bookService.Get(id);
        return book == null ? NotFound() : Ok(_mapper.Map<BookExtendDto>(book));
    }

    [HttpGet("extend")]
    public async Task<ActionResult<IEnumerable<BookExtendDto>>> GetExtendAll()
    {
        var books = await _bookService.Get();
        return Ok(_mapper.Map<IEnumerable<BookExtendDto>>(books));
    }

    [HttpPost]
    public async Task<ActionResult<BookExtendDto>> Create([FromBody] BookAddDto dto)
    {
        if (dto.PublishedYear > DateTime.Now.Year)
            return BadRequest("PublishedYear gələcək il ola bilməz.");
        if (dto.Stock == 0) dto.IsAvailable = false;

        var book = _mapper.Map<Book>(dto);
        var created = await _bookService.Add(book);
        var result = _mapper.Map<BookExtendDto>(created);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] BookAddDto dto)
    {
        if (dto.PublishedYear > DateTime.Now.Year)
            return BadRequest("PublishedYear gələcək il ola bilməz.");

        var book = await _bookService.Get(id);
        if (book == null) return NotFound();

        _mapper.Map(dto, book);
        await _bookService.Update(book);
        return NoContent();
    }

   
    [HttpPatch("{id:int}/stock")]
    public async Task<ActionResult> UpdateStock(int id, [FromBody] BookStockDto dto)
    {
        var book = await _bookService.Get(id);
        if (book == null) return NotFound();

        book.Stock = dto.Stock;
        book.IsAvailable = dto.Stock > 0;
        await _bookService.Update(book);
        return NoContent();
    }


    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var book = await _bookService.Get(id);
        if (book == null) return NotFound();
        await _bookService.Delete(book);
        return NoContent();
    }


    [HttpGet("search")]
    public async Task<ActionResult<List<BookDto>>> Search(string title)
    {
        var result = (await _bookService.Get()).Where(x => x.Title?.Contains(title, StringComparison.OrdinalIgnoreCase) == true);
        return Ok(_mapper.Map<List<BookDto>>(result));
    }

    [HttpGet("author")]
    public async Task<ActionResult<List<BookDto>>> GetByAuthor(string name)
    {
        var result = (await _bookService.Get()).Where(x => x.Author?.Contains(name, StringComparison.OrdinalIgnoreCase) == true);
        return Ok(_mapper.Map<List<BookDto>>(result));
    }

    [HttpGet("category/{category}")]
    public async Task<ActionResult<List<BookDto>>> GetByCategory(string category)
    {
        var result = (await _bookService.Get()).Where(x => x.Category?.Equals(category, StringComparison.OrdinalIgnoreCase) == true);
        return Ok(_mapper.Map<List<BookDto>>(result));
    }

    [HttpGet("filter")]
    public async Task<ActionResult<List<BookDto>>> Filter(decimal minPrice, decimal maxPrice)
    {
        var result = (await _bookService.Get()).Where(x => x.Price >= minPrice && x.Price <= maxPrice);
        return Ok(_mapper.Map<List<BookDto>>(result));
    }

    [HttpGet("available")]
    public async Task<ActionResult<List<BookDto>>> Available()
    {
        var result = (await _bookService.Get()).Where(x => x.Stock > 0 && x.IsAvailable);
        return Ok(_mapper.Map<List<BookDto>>(result));
    }

    [HttpGet("year/{year:int}")]
    public async Task<ActionResult<List<BookDto>>> GetByYear(int year)
    {
        var result = (await _bookService.Get()).Where(x => x.PublishedYear == year);
        return Ok(_mapper.Map<List<BookDto>>(result));
    }

    [HttpGet("sorted")]
    public async Task<ActionResult<List<BookDto>>> Sorted(string direction = "asc")
    {
        var books = await _bookService.Get();
        var result = direction.Equals("desc", StringComparison.OrdinalIgnoreCase)
            ? books.OrderByDescending(x => x.Price)
            : books.OrderBy(x => x.Price);
        return Ok(_mapper.Map<List<BookDto>>(result));
    }

    [HttpGet("statistics")]
    public async Task<ActionResult> Statistics()
    {
        var books = await _bookService.Get();
        if (books.Count == 0) return Ok(new { TotalBooks = 0, TotalStock = 0, AveragePrice = 0, MinPrice = 0, MaxPrice = 0 });

        return Ok(new
        {
            TotalBooks = books.Count,
            TotalStock = books.Sum(x => x.Stock),
            AveragePrice = books.Average(x => x.Price),
            MinPrice = books.Min(x => x.Price),
            MaxPrice = books.Max(x => x.Price)
        });
    }
}
