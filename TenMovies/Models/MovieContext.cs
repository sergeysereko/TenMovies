using Microsoft.EntityFrameworkCore;

namespace TenMovies.Models
{
    public class MovieContext: DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public MovieContext(DbContextOptions<MovieContext> options)
           : base(options)
        {
            if (Database.EnsureCreated())
            {
                Movies.Add(new Movie { Name = "Киндзадза", Director = "Трататат", Genre = "nfhffhfh", Year = 1986, Poster="nhfbhfdbj", Description="bdsgfuyejgv" });    
                SaveChanges();
            }
        }
    }
}



