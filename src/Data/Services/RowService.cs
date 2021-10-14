using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Core.ConfigurationOptions;
using Core.Contracts;
using Core.DatabaseEntities;
using Core.DTOs.Row;
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

        public async Task<int> CreateRowAsync(CreateRowRequest createRowRequest, CancellationToken cancellationToken)
        {
            Guard.Against.Null(createRowRequest, nameof(createRowRequest));
            Guard.Against.Default(createRowRequest.Week, nameof(createRowRequest.Week));

            await using var connection = new SqlConnection(_configuration.ConnectionString);
            await connection.OpenAsync(cancellationToken);

            var row = new Row { Week = createRowRequest.Week, };

            var result = await connection.InsertAsync<Row>(row);

            return result;
        }
    }
}
