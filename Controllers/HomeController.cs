using Microsoft.AspNetCore.Mvc;
using FullStackMasteryBootcampApp.Data;
using Microsoft.EntityFrameworkCore;

namespace FullStackMasteryBootcampApp.Controllers;

public class HomeController : Controller
{
    private readonly DataContext _context;
    public HomeController(DataContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        var movies = await _context.Movies.ToListAsync();
        var homeMovie = movies.FirstOrDefault(m => m.IsHome && m.IsActive);

        ViewBag.HomeMovie = homeMovie;

        return View(movies);
    }
}
