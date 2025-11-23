using API_MOVIE.DAL.Models.Dto;

namespace API_MOVIE.Services.IServices
{
    public interface IMovieService
    {
        Task<ICollection<MovieDto>> GetCategoriesAsync();//me retorna una lista de peliculas
        Task<MovieDto> GetCategoryAsync(int id);//me retorna un registro por su id

        Task<bool> CategoryExistByidAsync(int id);

        Task<bool> CategoryExistByNameAsync(string name);

        Task<MovieDto> CreateCategoryAsync(MovieUpdateDto categoryCreateDto);//me crea una pelicula

        Task<MovieDto> updateCategoryAsync(MovieUpdateDto dto, int id);//me actualiza una pelicula

        Task<bool> DeleteCategoryAsync(int id);//me borra una pelicula
    }
}
