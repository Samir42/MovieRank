using Microsoft.AspNetCore.Mvc;
using MovieRank.Contracts;
using MovieRank.Core.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieRank.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRankService _movieService;

        public MoviesController(IMovieRankService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IEnumerable<MovieResponse>> GetAllMovies()
        {
            var moviesFromServiceToReturn = await _movieService.GetAllAsync();

            return moviesFromServiceToReturn;
        }


        [HttpGet]
        [Route("{userId}/{movieName}")]
        public async Task<MovieResponse> GetMovie(int userId, string movieName)
        {
            var movieFromServiceToReturn = await _movieService.GetMovieAsync(userId, movieName);

            return movieFromServiceToReturn;
        }


        [HttpGet]
        [Route("user/{userId}/rankedMovies/{movieName}")]
        public async Task<IEnumerable<MovieResponse>> GetRankedMovies(int userId, string movieName)
        {
            var rankedMoviesToReturn = await _movieService.GetUserRankedMoviesByMovieTitle(userId, movieName);

            return rankedMoviesToReturn;
        }

        [HttpPost]
        [Route("{userId}")]
        public async Task<IActionResult> AddMovie(int userId, [FromBody] MovieRankRequest movieToCreate)
        {
            await _movieService.AddAsync(userId, movieToCreate);

            return Ok();
        }

        [HttpPatch]
        [Route("{userId}")]
        public async Task<IActionResult> AddMovie(int userId, [FromBody] MovieUpdateRequest movieToUpdate)
        {
            await _movieService.UpdateAsync(userId, movieToUpdate);

            return Ok();
        }

        [HttpGet]
        [Route("{movieName}/ranking")]
        public async Task<MovieRankResponse> GetMoviesRanking(string movieName)
        {
            var result = await _movieService.GetMovieRank(movieName);

            return result;
        }
    }
}
