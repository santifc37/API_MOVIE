using API_MOVIE.DAL.Models;
using API_MOVIE.Repository.IRepository;

namespace API_MOVIE.Repository
{
    public class MovieRepository : IMovieRepository
    {
        public Task<bool> CreateMovieAsync(Movie movie)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteMovieAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> GetMovieAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Movie>> GetMoviesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> MovieExistByidAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MovieExistByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateMovieAsync(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
