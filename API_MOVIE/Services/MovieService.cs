using API_MOVIE.DAL.Models;
using API_MOVIE.DAL.Models.Dto;
using API_MOVIE.Repository;
using API_MOVIE.Repository.IRepository;
using API_MOVIE.Services.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;

namespace API_MOVIE.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        
        
        //mapper
        public MovieService(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        //CREATE
        public async Task<MovieDto> CreateMovieAsync(MovieCreateDto movieCreateDto)
        {
            //Verificar si otra pelicula contiene el nombre
            var movieExist = await _movieRepository.MovieExistByNameAsync(movieCreateDto.name);

            if (movieExist)
            {
                //Otra pelicula con el mismo nombre
                throw new InvalidOperationException($"Ya existe una Peliicula con el nombre '{movieCreateDto.name}'");
            }

            //Crea el registro de la pelicula
            var movie = _mapper.Map<Movie>(movieCreateDto);

            var movieCreated = await _movieRepository.CreateMovieAsync(movie);

            if (!movieCreated)
            {
                //Error no se pudo crear
                throw new InvalidOperationException("Ocurrió un error al crear");
            }

            //lo crea en dto para devolver al usuario
            var movieDto = _mapper.Map<MovieDto>(movie);
            return movieDto;
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {

            //Verifica que si existe la pelicula
            var existingMovie = await _movieRepository.GetMovieAsync(id);

            if (existingMovie == null)
            {
                //No existe la pelicula
                throw new KeyNotFoundException($"No se encontró la categoría con la id {id}");

            }

            //Borra el registro
            var moviedelete = await _movieRepository.DeleteMovieAsync(id);

            if (!moviedelete)
            {

                //No se pudo borrar
                throw new InvalidOperationException("Ocurrió un error al borrar la categoria");
            }
            //devuelve el registro borrado
            return moviedelete;
        }

        public async Task<MovieDto> GetMovieAsync(int id)
        {
            //Busca la pelicula, mapea a dto
            var movie = await _movieRepository.GetMovieAsync(id);
            return _mapper.Map<MovieDto>(movie);
        }

        public async  Task<ICollection<MovieDto>> GetMoviesAsync()
        {
            //Busca todas las pelculas, mapea a dto
            var movies = await _movieRepository.GetMoviesAsync(); 
            return _mapper.Map<ICollection<MovieDto>>(movies); 
        }

        public Task<bool> MovieExistByidAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MovieExistByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<MovieDto> UpdateMovieAsync(MovieUpdateDto dto, int id)
        {
            //Verificar que si existe
            var existingMovie = await _movieRepository.GetMovieAsync(id);
            if (existingMovie == null)
            {
                //no existe
                throw new KeyNotFoundException($"No se encontró la categoría con la id {id}");
            }

            //Verificar si el nuevo nombre ya está en uso por otra categoria
            var movieExistbyName = await _movieRepository.MovieExistByNameAsync(dto.name);

            if (movieExistbyName)
            {
                //otra pelicula con el mismo nombre
                throw new InvalidOperationException($"Ya existe una categoria con nombre {dto.name}");
            }
            //manda la pelicula dto para actualizar
            _mapper.Map(dto, existingMovie);
            var categoryUpdate = await _movieRepository.UpdateMovieAsync(existingMovie);

            if (!categoryUpdate)
            {
                //error al actualizar
                throw new InvalidOperationException("Ocurrió un error en actualizar la categoria");
            }
            return _mapper.Map<MovieDto>(existingMovie);
        }
    }
    }

