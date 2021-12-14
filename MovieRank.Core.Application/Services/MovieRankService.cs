using MovieRank.Contracts;
using MovieRank.Core.Application.Interfaces;
using MovieRank.Core.Application.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRank.Core.Application.Services
{
    public class MovieRankService : IMovieRankService
    {
        private readonly IMovieRankRepository _movieRepository;
        private readonly IMapper _map;

        public MovieRankService(IMovieRankRepository movieRepository, IMapper map)
        {
            _movieRepository = movieRepository;
            _map = map;
        }

        public async Task AddAsync(int userId, MovieRankRequest movieToCreate)
        {
            var movie = _map.ToMovieModel(userId, movieToCreate);

            await _movieRepository.AddAsync(movie);
        }

        public async Task<IEnumerable<MovieResponse>> GetAllAsync()
        {
            var moviesFromDb = await _movieRepository.GetAllAsync();

            return _map.ToMovieContract(moviesFromDb);
        }

        public async Task<MovieResponse> GetMovieAsync(int userId, string movieName)
        {
            var movieFromDb = await _movieRepository.GetMovieAsync(userId, movieName);

            return _map.ToMovieContract(movieFromDb);
        }

        public async Task<MovieRankResponse> GetMovieRank(string movieName)
        {
            var moviesFromDb = await _movieRepository.GetMovieRankAsync(movieName);

            var overallMovieRanking = Math.Round(moviesFromDb.Select(x => x.Ranking).Average());

            return new MovieRankResponse
            {
                MovieName = movieName,
                OverallRanking = overallMovieRanking
            };
        }

        public async Task<IEnumerable<MovieResponse>> GetUserRankedMoviesByMovieTitle(int userId, string movieName)
        {
            var rankedMoviesFromDb = await _movieRepository.GetUserRankedMoviesByMovieTitle(userId, movieName);

            return _map.ToMovieContract(rankedMoviesFromDb);
        }

        public async Task UpdateAsync(int userId, MovieUpdateRequest movieToUpdate)
        {
            var existingMovieFromDb = await _movieRepository.GetMovieAsync(userId, movieToUpdate.MovieName);

            var movieUpdateModel = _map.ToMovieModel(userId, existingMovieFromDb, movieToUpdate);

            await _movieRepository.UpdateAsync(movieUpdateModel);
        }
    }
}
