using System.ComponentModel.DataAnnotations;

namespace FullStackMasteryBootcampApp.Data;

public class Movie
{
    [Key]
    public int MovieId { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsHome { get; set; } = false;
    public bool IsActive { get; set; } = true;
}