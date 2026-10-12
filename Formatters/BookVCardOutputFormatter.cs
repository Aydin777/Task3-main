using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;
using Task3.Dtos;

namespace Task3.Formatters;

public class BookVCardOutputFormatter : TextOutputFormatter
{
    public BookVCardOutputFormatter()
    {
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard"));
        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.Unicode);
    }

    protected override bool CanWriteType(Type? type)
        => typeof(BookExtendDto).IsAssignableFrom(type) ||
           typeof(IEnumerable<BookExtendDto>).IsAssignableFrom(type);

    private static Task WriteVCard(BookExtendDto book, HttpResponse response)
        => response.WriteAsync($"BEGIN:VCARD\nVERSION:3.0\nFN:{book.Title}\nNOTE:Author = {book.Author}\nNOTE:Category = {book.Category}\nNOTE:PublishedYear = {book.PublishedYear}\nNOTE:Age = {book.Age}\nEND:VCARD\n");

    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    {
        var response = context.HttpContext.Response;
        if (context.Object is IEnumerable<BookExtendDto> books)
        {
            foreach (var book in books) await WriteVCard(book, response);
        }
        else if (context.Object is BookExtendDto book)
        {
            await WriteVCard(book, response);
        }
    }
}
