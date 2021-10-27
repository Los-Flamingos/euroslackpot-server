using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Ardalis.Result;
using Core.ConfigurationOptions;
using Core.Contracts;
using Core.DatabaseEntities;
using Core.DTOs.Row;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Data.Services
{
    public class RowService : IRowService
    {
        private readonly DatabaseConfigurationOptions _configuration;
        private readonly ILogger _logger;

        public RowService (IOptions<DatabaseConfigurationOptions> options, ILogger<RowService> logger)
        {
            Guard.Against.Null(options, nameof(options));
            Guard.Against.Null(options.Value, nameof(options.Value));

            _configuration = options.Value;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<Result<int>> CreateRowAsync(CancellationToken cancellationToken)
        {
            await using var connection = new SqlConnection(_configuration.ConnectionString);
            await connection.OpenAsync(cancellationToken);
            var result = await connection.InsertAsync<Row>(new Row {});

            return result;
        }

        /// <inheritdoc />
        public async Task<Result<int>> UpdateRowAsync(UpdateRowRequest updateRowRequest, CancellationToken cancellationToken)
        {
            Guard.Against.Null(updateRowRequest, nameof(updateRowRequest), $"{nameof(updateRowRequest)} can't be null");

            await using var connection = new SqlConnection(_configuration.ConnectionString);
            await connection.OpenAsync(cancellationToken);

            var row = await connection.GetAsync<Row>(updateRowRequest.Id);
            if (row == null)
            {
                _logger.LogInformation("Row with id {RowId} was not found.", updateRowRequest.Id);
                return Result<int>.NotFound();
            }

            row.Earnings = updateRowRequest.UpdateRow.Earnings;
            await connection.UpdateAsync<Row>(row);

            return row.RowId;
        }
    }
}
