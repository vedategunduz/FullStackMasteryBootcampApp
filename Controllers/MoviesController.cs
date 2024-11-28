using FullStackMasteryBootcampApp.Data;
using FullStackMasteryBootcampApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FullStackMasteryBootcampApp.Controllers;

public class MoviesController : Controller
{
    private readonly DataContext _context;
    public MoviesController(DataContext context)
    {
        _context = context;
    }
    public async Task<ActionResult> Index()
    {
        var movies = await _context.Movies
       .Include(m => m.Category)
       .ToListAsync();

        return View(movies);
    }
    public async Task<IActionResult> ToggleIsHome(int id)
    {
        var movie = await _context.Movies.FindAsync(id);

        if (movie == null)
            return NotFound();

        movie.IsHome = !movie.IsHome;

        _context.Update(movie);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
    public async Task<IActionResult> ToggleIsActive(int id)
    {
        var movie = await _context.Movies.FindAsync(id);

        if (movie == null)
            return NotFound();

        movie.IsActive = !movie.IsActive;

        _context.Update(movie);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Delete(int id)
    {
        var movie = await _context.Movies.FindAsync(id);

        if (movie == null)
            return NotFound();

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Movie(int id)
    {
        var homeMovie = await _context.Movies
            .FirstOrDefaultAsync(m => m.MovieId == id && m.IsActive == true);

        if (homeMovie == null)
            return NotFound();

        return View(homeMovie);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var movie = await _context.Movies
            .FirstOrDefaultAsync(m => m.MovieId == id);

        if (movie == null)
            return NotFound();

        ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "CategoryId", "Title", movie.CategoryId);

        return View(movie);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(int id, Movie model, IFormFile ImageUrl)
    {
        if (id != model.MovieId)
            return NotFound();

        var movie = await _context.Movies.FindAsync(id);

        if (movie == null)
            return NotFound();

        movie.Title = model.Title;
        movie.Description = model.Description;
        movie.CategoryId = model.CategoryId;

        if (ImageUrl != null && ImageUrl.Length > 0)
        {
            if (!string.IsNullOrEmpty(movie.ImageUrl))
            {
                var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", movie.ImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageUrl.FileName);
            var filePath = Path.Combine(imagePath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await ImageUrl.CopyToAsync(stream);
            }

            movie.ImageUrl = "/images/" + uniqueFileName;
        }

        _context.Update(movie);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = await _context.Categories
        .Select(c => new SelectListItem
        {
            Value = c.CategoryId.ToString(),
            Text = c.Title
        })
        .ToListAsync();

        return View(new Movie());
    }
    [HttpPost]
    public async Task<IActionResult> Create(Movie model, IFormFile ImageUrl)
    {
        if (ImageUrl != null && ImageUrl.Length > 0)
        {
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageUrl.FileName);
            var filePath = Path.Combine(imagePath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await ImageUrl.CopyToAsync(stream);
            }

            model.ImageUrl = "/images/" + uniqueFileName;
        }

        _context.Movies.Add(model);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index", "Home");
    }
}