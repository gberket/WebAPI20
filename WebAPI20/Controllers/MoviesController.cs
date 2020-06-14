using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI20.Data.Interfaces;
using WebAPI20.Models;

namespace WebAPI20.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private const int DEFAULT_PAGESIZE = 2;
        private const int DEFAULT_PAGEINDEX = 1;

        private readonly IMoviesRepositorycs _moviesRepository;

        public MoviesController(IMoviesRepositorycs moviesRepository)
        {
            this._moviesRepository = moviesRepository;
        }

        //[HttpGet]
        //public async Task<ActionResult> GetMovies([FromQuery]int pageNumber = DEFAULT_PAGEINDEX, int pageSize = DEFAULT_PAGESIZE)
        //{
        //    try
        //    {
        //        return Ok(await _moviesRepository.GetMoviesPaginated(pageNumber, pageSize));
        //    }
        //    catch
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
        //    }
        //}

        [HttpGet]
        public async Task<ActionResult> GetMovies()
        {
            try
            {
                return Ok(await _moviesRepository.GetMovies());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            try
            {
                var result = await _moviesRepository.GetMovie(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> AddMovie(Movie movie)
        {
            try
            {
                if (movie == null)
                {
                    return BadRequest();
                }
                var createdMovie = await _moviesRepository.AddMovie(movie);
                return CreatedAtAction(nameof(GetMovie), new { id = createdMovie.Id }, createdMovie);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Movie>> UpdateMovie(int id, Movie movie)
        {
            try
            {
                if (id != movie.Id)
                {
                    return BadRequest();
                }
                var movieToUpdate = await _moviesRepository.GetMovie(id);
                if (movieToUpdate == null)
                {
                    return NotFound($"Movie with Id = {id} not found.");
                }
                return await _moviesRepository.UpdateMovie(movie);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Movie>> DeleteMovie(int id)
        {
            try
            {
                await _moviesRepository.DeleteMovie(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }
    }
}
