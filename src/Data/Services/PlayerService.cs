using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Core.ConfigurationOptions;
using Core.Contracts;
using Core.DatabaseEntities;
using Core.DTOs.Player;
using Dapper;
using Microsoft.Extensions.Options;

namespace Data.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly DatabaseConfigurationOptions _configuration;

        public PlayerService(IOptions<DatabaseConfigurationOptions> options)
        {
            Guard.Against.Null(options, nameof(options));
            Guard.Against.Null(options.Value, nameof(options.Value));

            _configuration = options.Value;
        }

        /// <summary>
        /// GetAllPlayersAsync asynchronously retrieves all players from database
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GetAllPlayersResponse>> GetAllPlayersAsync(CancellationToken cancellationToken)
        {
            await using var connection = new SQLiteConnection(_configuration.ConnectionString);
            await connection.OpenAsync(cancellationToken);

            var result = await connection.GetListAsync<Player>();

            return result.Select(x => new GetAllPlayersResponse
            {
                Id = x.Id,
                Name = x.Name,
            });
        }

        /// <summary>
        /// GetPlayerByIdAsync asynchronously returns player with given id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns player with id, or null if id does not exist</returns>
        public async Task<GetPlayerByIdResponse> GetPlayerByIdAsync(int id, CancellationToken cancellationToken)
        {
            await using var connection = new SQLiteConnection(_configuration.ConnectionString);
            await connection.OpenAsync(cancellationToken);

            var player = await connection.GetAsync<Player>(id);
            if (player == null)
            {
                return null;
            }

            return new GetPlayerByIdResponse
            {
                Id = player.Id,
                Name = player.Name,
            };
        }

        /// <summary>
        /// CreatePlayerAsync asynchronously creates a new player
        /// </summary>
        /// <param name="createPlayerRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns id of newly created entity</returns>
        public async Task<int> CreatePlayerAsync(CreatePlayerRequest createPlayerRequest, CancellationToken cancellationToken)
        {
            Guard.Against.Null(createPlayerRequest, nameof(createPlayerRequest));
            Guard.Against.NullOrEmpty(createPlayerRequest.Name, nameof(createPlayerRequest.Name));

            await using var connection = new SQLiteConnection(_configuration.ConnectionString);
            await connection.OpenAsync(cancellationToken);
            
            var player = new Player { Name = createPlayerRequest.Name };

            var result = await connection.InsertAsync<Player>(player);
            return result.Value;
        }
    }
}
