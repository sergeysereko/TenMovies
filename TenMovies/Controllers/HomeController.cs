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
        /*[ValidateAntiForgeryToken]  Не понял как это можно реализовать??*/
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


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.Movies == null)
            {
                return NotFound();
            }

            var movie = await db.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Director,Genre,Year,Poster,Description")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(movie);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        private bool MovieExists(int id)
        {
            return (db.Movies?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.Movies == null)
            {
                return NotFound();
            }

            var movie = await db.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }


        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (db.Movies == null)
            {
                return Problem("Entity set 'MovieContext.Movies'  is null.");
            }
            var movie = await db.Movies.FindAsync(id);
            if (movie != null)
            {
                db.Movies.Remove(movie);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.Movies == null)
            {
                return NotFound();
            }

            var movie = await db.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }


    }
}