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
using Core.Entities;
using Dapper;
using Dapper.Contrib.Extensions;
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

        public async Task<IEnumerable<GetAllPlayersResponse>> GetAllPlayersAsync(CancellationToken cancellationToken)
        {
            await using var connection = new SQLiteConnection(_configuration.ConnectionString);
            await connection.OpenAsync(cancellationToken);
            var result = await connection.GetAllAsync<Player>();
            return result.Select(x => new GetAllPlayersResponse
            {
                Id = x.PlayerId,
                Name = x.Name,
            });
        }

        public async Task<GetPlayerByIdResponse> GetPlayerByIdAsync(int id, CancellationToken cancellationToken)
        {
            await using var connection = new SQLiteConnection(_configuration.ConnectionString);
            await connection.OpenAsync(cancellationToken);

            var result = await connection.GetAsync<Player>(id);
            return new GetPlayerByIdResponse
            {
                Id = result.PlayerId,
                Name = result.Name,
            };
        }

        public async Task<int> CreatePlayerAsync(CreatePlayerRequest createPlayerRequest, CancellationToken cancellationToken)
        {
            Guard.Against.Null(createPlayerRequest, nameof(createPlayerRequest));
            Guard.Against.NullOrEmpty(createPlayerRequest.Name, nameof(createPlayerRequest.Name));

            await using var connection = new SQLiteConnection(_configuration.ConnectionString);
            await connection.OpenAsync(cancellationToken);
            
            var player = new Player { Name = createPlayerRequest.Name };

            return await connection.InsertAsync(player);
        }
    }
}
