using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Core.ConfigurationOptions;
using Core.Contracts;
using Core.DatabaseEntities;
using Core.DTOs.Row;
using Core.Validators;
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

        public async Task<Result<int>> CreateRowAsync(CreateRowRequest createRowRequest, CancellationToken cancellationToken)
        {
            Guard.Against.Null(createRowRequest, nameof(createRowRequest));

            await using var connection = new SqlConnection(_configuration.ConnectionString);
            await connection.OpenAsync(cancellationToken);

            var row = new Row { Earnings = createRowRequest.Earnings, };

            var result = await connection.InsertAsync<Row>(row);

            return result;
        }
    }
}
