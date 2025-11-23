using API_MOVIE.DAL.Models;

namespace API_MOVIE.Repository.IRepository
{
    public interface IMovieRepository
    {

        Task<ICollection<Movie>> GetMoviesAsync();

        Task<Movie> GetMovieAsync(int id);

        Task<bool> MovieExistByidAsync(int id);

        Task<bool> MovieExistByNameAsync(string name);

        Task<bool> CreateMovieAsync(Movie movie);

        Task<bool> UpdateMovieAsync(Movie movie);

        Task<bool> DeleteMovieAsync(int id);
    }
}
