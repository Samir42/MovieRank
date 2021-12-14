using MovieRank.Core.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRank.Core.Application.Interfaces
{
    public interface IMovieRankRepository
    {
        Task<IEnumerable<Movie>> GetAllAsync();

        Task<Movie> GetMovieAsync(int userId, string movieName);

        Task<IEnumerable<Movie>> GetUserRankedMoviesByMovieTitle(int userId, string movieName);

        Task AddAsync(Movie movieToAdd);

        Task UpdateAsync(Movie movieUpdateModel);

        Task<IEnumerable<Movie>> GetMovieRankAsync(string movieName);
    }
}
