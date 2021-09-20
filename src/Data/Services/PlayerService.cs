using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading;
using System.Threading.Tasks;
using Core.ConfigurationOptions;
using Core.Contracts;
using Core.Entities;
using Dapper;
using Microsoft.Extensions.Options;

namespace Data.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly DatabaseConfigurationOptions _configuration;

        public PlayerService(IOptions<DatabaseConfigurationOptions> options)
        {
            _configuration = options.Value;
        }

        public async Task<IEnumerable<Player>> GetAllPlayersAsync(CancellationToken cancellationToken)
        {
            const string sql = "SELECT * FROM player";
            await using var connection = new SQLiteConnection(_configuration.ConnectionString);
            await connection.OpenAsync(cancellationToken);
            var result = await connection.QueryAsync<Player>(sql);
            return result;
        }

        public async Task<Player> GetPlayerByIdAsync(int id, CancellationToken cancellationToken)
        {
            const string sql = "SELECT * FROM player where id = @id";
            await using var connection = new SQLiteConnection(_configuration.ConnectionString);
            await connection.OpenAsync(cancellationToken);
            var result = await connection.QuerySingleAsync<Player>(sql, new { id });
            return result;
        }
    }
}
