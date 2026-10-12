using System.ComponentModel.DataAnnotations;

namespace Task3.Dtos;

public class BookStockDto
{
    [Range(0, int.MaxValue)]
    public int Stock { get; set; }
}
