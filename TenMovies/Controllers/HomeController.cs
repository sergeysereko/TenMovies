using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TenMovies.Models;


namespace TenMovies
{
    public class HomeController : Controller

    {
       
        MovieContext db;
        IWebHostEnvironment _appEnvironment;
        public HomeController(MovieContext context, IWebHostEnvironment appEnvironment)
        {
            db = context;
            _appEnvironment = appEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Movie> movies = await Task.Run(() => db.Movies);
            ViewBag.Movies = movies;
            return View();
        }

        public async Task<IActionResult> Admin()
        {
            IEnumerable<Movie> movies = await Task.Run(() => db.Movies);
            ViewBag.Movies = movies;
            return View();
        }

       
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        /*[ValidateAntiForgeryToken]*/
        public async Task<IActionResult> CreateMovie(IFormFile Poster,[Bind("Id,Name,Director,Genre,Year,Poster,Description")] Movie movie)
        {
            if (Poster != null)
            {
                // Путь к папке Files
                string path = "/Image/" + Poster.FileName; // имя файла
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await Poster.CopyToAsync(fileStream); // копируем файл в поток
                }
                movie.Poster = "~" + path;


                if (movie != null)
                {
                    db.Add(movie);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else 
                {
                    return View(movie);
                }
            }

            return View(movie);

        }

    }
}