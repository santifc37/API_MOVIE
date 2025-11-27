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
            if (await _movieRepository.MovieExistByNameAsync(movieCreateDto.name))
                throw new InvalidOperationException(
                    $"Ya existe una Película con el nombre '{movieCreateDto.name}'");

            var movie = _mapper.Map<Movie>(movieCreateDto);

            if (!await _movieRepository.CreateMovieAsync(movie))
                throw new InvalidOperationException("Ocurrió un error al crear la película.");

            return _mapper.Map<MovieDto>(movie);
        }

        //PELICULAS QUE DURAN MAS DE CIERTA CANTIDAD
        public async Task<ICollection<MovieDto>> GetMoviesLongerThanAsync(int seconds)
        {

            var movies = await _movieRepository.GetMoviesLongerThanAsync(seconds);

            return _mapper.Map<ICollection<MovieDto>>(movies);
        }

        //BORRAR PELICULA
        public async Task<bool> DeleteMovieAsync(int id)
        {
         
            var movie = await _movieRepository.GetMovieAsync(id)
                ?? throw new KeyNotFoundException($"No se encontró la película con id {id}");

            
            if (!await _movieRepository.DeleteMovieAsync(id))
                throw new InvalidOperationException("No se pudo borrar la película");

            return true;
        }

        //BUSCAR PELICULA
        public async Task<MovieDto> GetMovieAsync(int id)
        {
           
            var movie = await _movieRepository.GetMovieAsync(id);
            return _mapper.Map<MovieDto>(movie);
        }

        //BUSCAR PELICULAS
        public async  Task<ICollection<MovieDto>> GetMoviesAsync()
        {
            
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

        //BUSCAR PELICULA POR NOMBRE
        public async Task<ICollection<MovieDto>> SearchMoviesByNameAsync(string name)
        {
            var movies = await _movieRepository.SearchMoviesByNameAsync(name);

            
            var moviesDto = _mapper.Map<ICollection<MovieDto>>(movies);
            return moviesDto;
        }

        //ACTUALIZAR UNA PELICULA
        public async Task<MovieDto> UpdateMovieAsync(MovieUpdateDto dto, int id)
        {
            var movie = await _movieRepository.GetMovieAsync(id)
                ?? throw new KeyNotFoundException($"Película con id {id} no encontrada.");

            if (await _movieRepository.MovieExistByNameAsync(dto.name))
                throw new InvalidOperationException($"Ya existe una película con el nombre '{dto.name}'.");

            _mapper.Map(dto, movie);

            if (!await _movieRepository.UpdateMovieAsync(movie))
                throw new InvalidOperationException("No se pudo actualizar la película.");

            return _mapper.Map<MovieDto>(movie);
        }



    }
}

