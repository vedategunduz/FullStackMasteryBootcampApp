using Microsoft.EntityFrameworkCore;

namespace FullStackMasteryBootcampApp.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Movie> Movies => Set<Movie>();
}