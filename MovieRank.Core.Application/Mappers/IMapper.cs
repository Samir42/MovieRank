using MovieRank.Contracts;
using MovieRank.Core.Model.Models;
using System.Collections.Generic;

namespace MovieRank.Core.Application.Mappers
{
    public interface IMapper
    {
        IEnumerable<MovieResponse> ToMovieContract(IEnumerable<Movie> movies);

        MovieResponse ToMovieContract(Movie movie);

        Movie ToMovieModel(int userId, MovieRankRequest movieToCreate);

        Movie ToMovieModel(int userId, Movie movieToCreate, MovieUpdateRequest movieUpdateReuqest);
    }
}
