using API_MOVIE.DAL.Models;
using API_MOVIE.DAL.Models.Dto;
using AutoMapper;

namespace API_MOVIE.mapper
{
    public class Mapper
    {

        public class Mappers : Profile
        {

            public Mappers()
            {
                CreateMap<Movie, MovieDto>().ReverseMap();
                CreateMap<Movie, MovieCreateDto>().ReverseMap();
                CreateMap<Movie, MovieUpdateDto>().ReverseMap();
            }
        }
    }
}
