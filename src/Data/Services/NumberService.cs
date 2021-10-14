using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Common.Exceptions;
using Core.ConfigurationOptions;
using Core.Contracts;
using Core.DatabaseEntities;
using Core.DTOs.Number;
using Dapper;
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

        public async Task<GetNumbersForRowResponse> GetNumbersForRowAsync(int rowId, CancellationToken cancellationToken)
        {
            await using var connection = new SqlConnection(_configuration.ConnectionString);
            await connection.OpenAsync(cancellationToken);

            var result = await connection.QueryAsync<Number>("SELECT * FROM Number WHERE RowId = @rowId", rowId);
            if (result == null)
            {
                return null;
            }

            return new GetNumbersForRowResponse
            {
                Id = rowId,
                Week = result.FirstOrDefault().Week,
                Numbers = result.Select(x => new NumberRowDto
                {
                    NumberId = x.NumberId,
                    PlayerId = x.PlayerId,
                    Type = x.Type,
                    Value = x.Value,
                }).ToList(),
            };
        }

        public async Task<GetNumbersForWeekResponse> GetNumbersForWeekAsync(int week, CancellationToken cancellationToken)
        {
            await using var connection = new SqlConnection(_configuration.ConnectionString);
            await connection.OpenAsync(cancellationToken);

            var result = await connection.QueryAsync<Number>("SELECT * FROM Number where Week = @week", week);

            return new GetNumbersForWeekResponse
            {
                Week = week,
                Numbers = result.Select(x => new NumberWeekDto
                    {
                        NumberId = x.NumberId,
                        PlayerId = x.PlayerId,
                        RowId = x.RowId,
                        Type = x.Type,
                        Value = x.Value,
                    })
                    .ToList(),
            };
        }

        public async Task<int> CreateNumberAsync(CreateNumberRequest createNumberRequest, CancellationToken cancellationToken)
        {
            Guard.Against.Null(createNumberRequest, nameof(createNumberRequest));
            Guard.Against.Default(createNumberRequest.NumberRequest.Week, nameof(createNumberRequest.NumberRequest.Week));
            Guard.Against.Default(createNumberRequest.NumberRequest.PlayerId, nameof(createNumberRequest.NumberRequest.PlayerId));
            Guard.Against.Default(createNumberRequest.RowId, nameof(createNumberRequest.RowId));
            Guard.Against.Default(createNumberRequest.NumberRequest.Value, nameof(createNumberRequest.NumberRequest.Value));

            await using var connection = new SqlConnection(_configuration.ConnectionString);
            await connection.OpenAsync(cancellationToken);

            var result = await connection.GetAsync<Row>(createNumberRequest.RowId);
            if (result == null)
            {
                throw new RowNotFoundException($"Row with id {createNumberRequest.RowId} could not be found");
            }

            // TODO: Error handling for duplicate numbers, already max amount of numbers etc.
            var number = new Number
            {
                Week = createNumberRequest.NumberRequest.Week,
                RowId = createNumberRequest.RowId,
                PlayerId = createNumberRequest.NumberRequest.PlayerId,
                Type = createNumberRequest.NumberRequest.Type,
                Value = createNumberRequest.NumberRequest.Value,
            };

            return await connection.InsertAsync(number);
        }
    }
}