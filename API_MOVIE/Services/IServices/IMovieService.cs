using API_MOVIE.DAL.Models;
using API_MOVIE.DAL.Models.Dto;

namespace API_MOVIE.Services.IServices
{
    public interface IMovieService
    {
        Task<ICollection<MovieDto>> GetMoviesAsync();//me retorna una lista de peliculas
        Task<MovieDto> GetMovieAsync(int id);//me retorna un registro por su id

        Task<bool> MovieExistByidAsync(int id);

        Task<bool> MovieExistByNameAsync(string name);

        Task<ICollection<MovieDto>> SearchMoviesByNameAsync(string name);

        Task<ICollection<MovieDto>> GetMoviesLongerThanAsync(int seconds);


        Task<MovieDto> CreateMovieAsync(MovieCreateDto movieCreateDto);//me crea una pelicula

        Task<MovieDto> UpdateMovieAsync(MovieUpdateDto dto, int id);//me actualiza una pelicula

        Task<bool> DeleteMovieAsync(int id);//me borra una pelicula
    }
}
