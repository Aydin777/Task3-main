using System.ComponentModel.DataAnnotations;

namespace Task3.Entities;

public class Book
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string? Title { get; set; }

    [Required]
    [MaxLength(150)]
    public string? Author { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Category { get; set; }

    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int PageCount { get; set; }
    public int PublishedYear { get; set; }
    public bool IsAvailable { get; set; }
}
