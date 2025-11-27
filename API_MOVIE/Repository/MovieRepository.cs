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
          
            var movieToDelete = await GetMovieAsync(id);

            if (movieToDelete is null)
            {
                return false;
            }

            _context.Movies.Remove(movieToDelete);
            var result = await _context.SaveChangesAsync();

            return result > 0;
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
                .OrderBy(c => c.id)
                .ToListAsync();

            return movies;
        }

        //Consultar peliculas por el genero
        public async Task<ICollection<Movie>> GetMoviesByGenreAsync(string genre)
        {
            return await _context.Movies
                .Where(m => m.genero.ToLower() == genre.ToLower())
                .ToListAsync();   
        }


        //Consultar peliculas por el año
        public async Task<ICollection<Movie>> GetMoviesByYearAsync(int year)
        {
            return await _context.Movies
                .Where(m => m.año == year)
                .ToListAsync();  // List<T> implementa ICollection<T>
        }


        //Existencia de pelicula por id
        public async Task<bool> MovieExistByidAsync(int id)
        {
            var MovieExists = await _context.Movies
                .AsNoTracking()
                .AnyAsync(c => c.id == id);

            return MovieExists;
        }

        //Existencia de pelicula por nombre
        public async Task<bool> MovieExistByNameAsync(string name)
        {
            var MoviesExists = await _context.Movies
                .AsNoTracking()
                .AnyAsync(c => c.name == name);

            return MoviesExists;
        }

        //Buscar peliculas por nombre 
        public async Task<ICollection<Movie>> SearchMoviesByNameAsync(string name)
        {
            return await _context.Movies
                .AsNoTracking()
                .Where(c => c.name.ToLower().Contains(name.ToLower()))
                .OrderBy(c => c.name)
                .ToListAsync();
        }

        //Peliculas con cierta cantidad de duracion
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
