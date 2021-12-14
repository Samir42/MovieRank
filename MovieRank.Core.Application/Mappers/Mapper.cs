using MovieRank.Contracts;
using MovieRank.Core.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieRank.Core.Application.Mappers
{
    public class Mapper : IMapper
    {
        public IEnumerable<MovieResponse> ToMovieContract(IEnumerable<Movie> movies)
        {
            return movies.Select(ToMovieContract);
        }

        public MovieResponse ToMovieContract(Movie movie)
        {
            return new MovieResponse
            {
                MovieName = movie?.MovieName,
                Description = movie?.Description,
                Actors = movie?.Actors,
                Ranking = movie is null ? default : movie.Ranking,
                RankedDateTime = movie?.RankedDateTime
            };
        }

        public Movie ToMovieModel(int userId, MovieRankRequest movieToCreate)
        {
            return new Movie
            {
                UserId = userId,
                MovieName = movieToCreate?.MovieName,
                Description = movieToCreate?.Description,
                Actors = movieToCreate?.Actors,
                Ranking = movieToCreate is null ? default : movieToCreate.Ranking,
                RankedDateTime = DateTime.UtcNow.ToString()

            };
        }

        public Movie ToMovieModel(int userId, Movie movieToCreate, MovieUpdateRequest movieUpdateReuqest)
        {
            return new Movie
            {
                UserId = userId,
                MovieName = movieToCreate?.MovieName,
                Description = movieToCreate?.Description,
                Actors = movieToCreate?.Actors,
                Ranking = movieUpdateReuqest.Ranking,
                RankedDateTime = DateTime.UtcNow.ToString()
            };
        }
    }
}
