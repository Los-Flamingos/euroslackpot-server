using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Core.ConfigurationOptions;
using Core.Contracts;
using Core.DatabaseEntities;
using Core.DTOs.Number;
using Core.Validators;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Data.Services
{
    public class NumberService : INumberService
    {
        private readonly DatabaseConfigurationOptions _configuration;
        private readonly ILogger<NumberService> _logger;

        public NumberService(IOptions<DatabaseConfigurationOptions> options, ILogger<NumberService> logger)
        {
            Guard.Against.Null(options, nameof(options));
            Guard.Against.Null(options.Value, nameof(options.Value));

            _logger = logger;
            _configuration = options.Value;
        }

        public async Task<Result<GetNumbersForWeekResponse>> GetNumbersForWeekAsync(int week, CancellationToken cancellationToken)
        {
            await using var connection = new SqlConnection(_configuration.ConnectionString);
            await connection.OpenAsync(cancellationToken);

            var result = await connection.QueryAsync<Number>("SELECT * FROM Number where Week = @week", week);

            return new Result<GetNumbersForWeekResponse>(new GetNumbersForWeekResponse
            {
                Week = week,
                Numbers = result.Select(x => new NumberWeekDto
                    {
                        NumberId = x.NumberId,
                        PlayerId = x.PlayerId,
                        RowId = x.RowId,
                        Type = x.NumberType,
                        Value = x.Value,
                    })
                    .ToList(),
            });
        }

        public async Task<Result<int>> CreateNumberAsync(CreateNumberRequest createNumberRequest, CancellationToken cancellationToken)
        {
            Guard.Against.Null(createNumberRequest, nameof(createNumberRequest));

            var validator = new CreateNumberRequestValidator();
            var validateResult = await validator.ValidateAsync(createNumberRequest, cancellationToken);
            if (!validateResult.IsValid)
            {
                return Result<int>.Invalid(validateResult.AsErrors());
            }

            await using var connection = new SqlConnection(_configuration.ConnectionString);
            await connection.OpenAsync(cancellationToken);

            var row = await connection.GetAsync<Row>(createNumberRequest.RowId);
            if (row == null)
            {
                _logger.LogError("Row with id {RowId} was not found", createNumberRequest.RowId);
                return Result<int>.Error($"Row with id {createNumberRequest.RowId} was not found");
            }

            // TODO: Error handling for duplicate numbers, already max amount of numbers etc.
            var number = new Number
            {
                Week = createNumberRequest.NumberRequest.Week,
                RowId = createNumberRequest.RowId,
                PlayerId = createNumberRequest.NumberRequest.PlayerId,
                NumberType = createNumberRequest.NumberRequest.Type,
                Value = createNumberRequest.NumberRequest.Value,
            };

            var result = await connection.InsertAsync(number);
            return Result<int>.Success(result);
        }
    }
}