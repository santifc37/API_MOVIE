using API_MOVIE.DAL;
using API_MOVIE.DAL.Models;
using API_MOVIE.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace API_MOVIE.Repository
{
    public class MovieRepository : IMovieRepository
    {
        public readonly AppDbContext _context;

        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }

        //Crear nuevo registro de pelicula
        public async Task<bool> CreateMovieAsync(Movie movie)
        {
            movie.CreatedDate = DateTime.UtcNow;

            await _context.Movies.AddAsync(movie);

            await _context.SaveChangesAsync();

            return true;
        }

        //Borrar registro de una pelicula por id
        public async Task<bool> DeleteMovieAsync(int id)
        {
            var movie = await GetMovieAsync(id);

            if (movie == null)
            {
                return false;
            }

            _context.Movies.Remove(movie);

            await _context.SaveChangesAsync();

            return true;
        }

        //Consultar registro por id
        public async Task<Movie> GetMovieAsync(int id)
        {
            var movie = await _context.Movies.AsNoTracking().FirstOrDefaultAsync(c => c.id == id);
            return movie;
        }

        //Consultar todos los registros ordenados por nombre
        public async Task<ICollection<Movie>> GetMoviesAsync()
        {
            var movies = await _context.Movies
                .AsNoTracking()
                .OrderBy(c => c.name)
                .ToListAsync();

            return movies;
        }


        public async Task<bool> MovieExistByidAsync(int id)
        {
            var MovieExists = await _context.Movies
                .AsNoTracking()
                .AnyAsync(c => c.id == id);

            return MovieExists;
        }

        public async Task<bool> MovieExistByNameAsync(string name)
        {
            var MoviesExists = await _context.Movies
                .AsNoTracking()
                .AnyAsync(c => c.name == name);

            return MoviesExists;
        }

        public async Task<ICollection<Movie>> SearchMoviesByNameAsync(string name)
        {
            return await _context.Movies
                .AsNoTracking()
                .Where(c => c.name.ToLower().Contains(name.ToLower()))
                .OrderBy(c => c.name)
                .ToListAsync();
        }

        public async Task<ICollection<Movie>> GetMoviesLongerThanAsync(int seconds)
        {
            return await _context.Movies
                .AsNoTracking()
                .Where(m => m.duration > seconds)
                .OrderBy(m => m.name)
                .ToListAsync();
        }



        //Actualizar un registro
        public async Task<bool> UpdateMovieAsync(Movie movie)
        {
            movie.UpdatedDate = DateTime.UtcNow;
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
