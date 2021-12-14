using MovieRank.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRank.Core.Application.Interfaces
{
    public interface IMovieRankService
    {
        Task<IEnumerable<MovieResponse>> GetAllAsync();

        Task<MovieResponse> GetMovieAsync(int userId, string movieName);

        Task<IEnumerable<MovieResponse>> GetUserRankedMoviesByMovieTitle(int userId, string movieName);

        Task AddAsync(int userId, MovieRankRequest movieToCreate);

        Task UpdateAsync(int userId, MovieUpdateRequest movieToUpdate);

        Task<MovieRankResponse> GetMovieRank(string movieName);
    }
}
