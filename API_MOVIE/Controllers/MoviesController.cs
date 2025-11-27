using API_MOVIE.DAL.Models;
using API_MOVIE.DAL.Models.Dto;
using API_MOVIE.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace API_MOVIE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("SearchAllMovies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICollection<MovieDto>>> GetMoviesIDAsync()
        {
            var movies = await _movieService.GetMoviesAsync();
           
            return Ok(movies);

        }

        [HttpGet("DurationMovies")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetLongMovies(int duration)
        {
            try
            {

                var movies = await _movieService.GetMoviesLongerThanAsync(duration);
                if (movies == null || movies.Count == 0)
                    return NotFound(new { message = $"No hay películas con duracion mayor a {duration} segundos" });

                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("SearchDescription")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<MovieDto>> GetDescriptionMovieAsync(int id)
        {
            var movie = await _movieService.GetMovieAsync(id);

            if (movie == null )
            {
                return NotFound(new { message = $"No hay películas con ID {id}" });
            }

            return Ok(movie.description);
        }

        [HttpGet("searchByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICollection<MovieDto>>> SearchMovies(string name)
        {
            var result = await _movieService.SearchMoviesByNameAsync(name);
            if (result == null || result.Count == 0) {
                return NotFound(new { message = $"No hay películas con nombre {name}" });
            }
            return Ok(result);
        }


        [HttpGet("SearchById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<MovieDto>> GetMovieAsync(int id)
        {
            var movie = await _movieService.GetMovieAsync(id);
            if (movie == null)
            {
                return NotFound(new { message = $"No hay películas con ID{id}" });
            }
            return Ok(movie);
        }

        [HttpPost(Name = "CreateMovieAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]

        public async Task<ActionResult<MovieDto>> CreateMovieAsync([FromBody] MovieCreateDto movieCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            try
            {
                var createMovie = await _movieService.CreateMovieAsync(movieCreateDto);
                return CreatedAtRoute(
                        "GetMovieAsync",
                        new { Id = createMovie.id },
                        createMovie
                    );
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("Ya existe"))
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut("{id:int}", Name = "UpdateMovieAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<MovieDto>> UpdateMovieAsync([FromBody] MovieUpdateDto dto, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            try
            {
                var updatedMovie = await _movieService.UpdateMovieAsync(dto, id);
                return Ok(updatedMovie);

            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpDelete("{id:int}", Name = "deleteMovieAsync")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult> DeleteCategoryAsync(int id)
        {

            try
            {
                var deletedMovie = await _movieService.DeleteMovieAsync(id);
                return Ok(deletedMovie);

            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }


        

    }
}
