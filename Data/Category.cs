using System.ComponentModel.DataAnnotations;

namespace FullStackMasteryBootcampApp.Data;

public class Category
{
    [Key]
    public int CategoryId { get; set; }
    public string? Title { get; set; }
}