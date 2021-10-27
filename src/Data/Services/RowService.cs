using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Ardalis.Result;
using Core.ConfigurationOptions;
using Core.Contracts;
using Core.DatabaseEntities;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Options;

namespace Data.Services
{
    public class RowService : IRowService
    {
        private readonly DatabaseConfigurationOptions _configuration;

        public RowService (IOptions<DatabaseConfigurationOptions> options)
        {
            Guard.Against.Null(options, nameof(options));
            Guard.Against.Null(options.Value, nameof(options.Value));

            _configuration = options.Value;
        }

        /// <inheritdoc />
        public async Task<Result<int>> CreateRowAsync(CancellationToken cancellationToken)
        {
            await using var connection = new SqlConnection(_configuration.ConnectionString);
            await connection.OpenAsync(cancellationToken);
            var result = await connection.InsertAsync<Row>(new Row {});

            return result;
        }
    }
}
