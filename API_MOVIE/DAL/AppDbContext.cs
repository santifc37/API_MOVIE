using API_MOVIE.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace API_MOVIE.DAL
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Movie> Movies { get; set; }

    }

}

