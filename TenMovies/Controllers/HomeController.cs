using Microsoft.AspNetCore.Mvc;

using TenMovies.Models;

namespace TenMovies
{
    public class HomeController : Controller

    {
       
        MovieContext db;
        public HomeController(MovieContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Movie> movies = await Task.Run(() => db.Movies);
            ViewBag.Movies = movies;
            return View("Index");
        }

    }
}