using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
    public class NumberService : INumberService
    {
        private readonly DatabaseConfigurationOptions _configuration;

        public NumberService(IOptions<DatabaseConfigurationOptions> options)
        {
            Guard.Against.Null(options, nameof(options));
            Guard.Against.Null(options.Value, nameof(options.Value));

            _configuration = options.Value;
        }

        public async Task<GetRowByIdResponse> GetRowByIdAsync(int request, CancellationToken cancellationToken)
        {
            await using var connection = new SqlConnection(_configuration.ConnectionString);
            await connection.OpenAsync(cancellationToken);
            var result = await connection.GetAsync<Number>(request);
            return new GetRowByIdResponse
            {
                Id = result.RowId,
                Week = result.Week,
            };
        }

        public async Task<int> CreateRowAsync(CreateRowRequest createRowRequest, CancellationToken cancellationToken)
        {
            Guard.Against.Null(createRowRequest, nameof(createRowRequest));
            Guard.Against.NullOrEmpty(createRowRequest.Week, nameof(createRowRequest.Week));

            await using var connection = new SqlConnection(_configuration.ConnectionString);
            await connection.OpenAsync(cancellationToken);
            
            var number = new Number
            {
                Week = createRowRequest.Week,
            };

            return await connection.InsertAsync(number);
        }

        public async Task<List<GetNumbersForWeekResponse>> GetNumbersForWeekAsync(int week, CancellationToken cancellationToken)
        {
            await using var connection = new SqlConnection(_configuration.ConnectionString);
            await connection.OpenAsync(cancellationToken);

            var result = await connection.GetAllAsync<Number>();

            return result.Select(x => new GetNumbersForWeekResponse
            {
                Id = x.RowId,
                Week = x.Week,
            }).ToList();
        }
    }
}