using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using MovieRank.Core.Application.Interfaces;
using MovieRank.Core.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRank.Core.Infrastructure.Repositories
{
    public class MovieRankRepository : IMovieRankRepository
    {
        private readonly DynamoDBContext _context;

        public MovieRankRepository(IAmazonDynamoDB dynamoDbClient)
        {
            _context = new DynamoDBContext(dynamoDbClient);
        }

        public async Task AddAsync(Movie movieToAdd)
        {
            await _context.SaveAsync(movieToAdd);
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await _context.ScanAsync<Movie>(new List<ScanCondition> { }).GetRemainingAsync();
        }

        public async Task<Movie> GetMovieAsync(int userId, string movieName)
        {
            return await _context.LoadAsync<Movie>(userId, movieName);
        }

        public async Task<IEnumerable<Movie>> GetMovieRankAsync(string movieName)
        {
            var config = new DynamoDBOperationConfig
            {
                IndexName = "MovieName-index"
            };

            return await _context.QueryAsync<Movie>(movieName, config).GetRemainingAsync();
        }

        public async Task<IEnumerable<Movie>> GetUserRankedMoviesByMovieTitle(int userId, string movieName)
        {
            var config = new DynamoDBOperationConfig
            {
                QueryFilter = new List<ScanCondition>
                {
                    new ScanCondition("MovieName", ScanOperator.BeginsWith, movieName)
                }
            };

            return await _context.QueryAsync<Movie>(userId, config).GetRemainingAsync();
        }

        public async Task UpdateAsync(Movie movieUpdateModel)
        {
            await _context.SaveAsync(movieUpdateModel);
        }
    }
}
