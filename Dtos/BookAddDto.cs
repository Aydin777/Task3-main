using System.ComponentModel.DataAnnotations;

namespace Task3.Dtos;

public class BookAddDto
{
    [Required]
    public string? Title { get; set; }
    [Required]
    public string? Author { get; set; }
    [Required]
    public string? Category { get; set; }
    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }
    [Range(0, int.MaxValue)]
    public int Stock { get; set; }
    [Range(1, int.MaxValue)]
    public int PageCount { get; set; }
    [Range(1000, 9999)]
    public int PublishedYear { get; set; }
    public bool IsAvailable { get; set; }
}
