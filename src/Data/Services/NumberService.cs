using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Core.ConfigurationOptions;
using Core.Contracts;
using Core.DatabaseEntities;
using Core.DTOs.Row;
using Dapper;
using Microsoft.Extensions.Options;
using Z.Dapper.Plus;

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

        /// <inheritdoc cref="INumberService.GetAllAsync"/>
        public async Task<List<GetAllNumbersResponse>> GetAllAsync(CancellationToken cancellationToken)
        {
            await using var connection = new SQLiteConnection(_configuration.ConnectionString);
            await connection.OpenAsync(cancellationToken);
            
            var result = await connection.GetListAsync<Number>();

            var numbers = result.ToList();
            if (!numbers.Any()) 
            {
                return null;
            }

            return numbers.GroupBy(x => x.Week).Select(x => new GetAllNumbersResponse
            {
                Week = x.Key,
                Numbers = x.Select(n => new GetNumbersForAllNumbersResponse()
                {
                    Id = n.Id,
                    RowId = n.RowId,
                    PlayerId = n.PlayerId,
                    Type = n.Type,
                    Value = n.Value,
                    
                }).ToList(),
            }).ToList();
        }

        /// <inheritdoc cref="INumberService.GetNumbersForRowIdAsync"/>
        public async Task<GetNumbersForRowIdResponse> GetNumbersForRowIdAsync(int id, CancellationToken cancellationToken)
        {
            Guard.Against.Default(id, nameof(id));

            await using var connection = new SQLiteConnection(_configuration.ConnectionString);
            await connection.OpenAsync(cancellationToken);
            
            var result = await connection.GetListAsync<Number>(new { RowId = id });

            var numbers = result.ToList();
            if (!numbers.Any()) 
            {
                return null;
            }

            return new GetNumbersForRowIdResponse
            {
                Week = numbers.First().Week,
                RowId = numbers.First().RowId,
                Numbers = numbers.Select(x => new GetNumberForRowIdNumberResponse
                {
                    Id = x.Id,
                    PlayerId = x.PlayerId,
                    Type = x.Type,
                    Value = x.Value,
                }).ToList(),
            };
        }

        /// <inheritdoc cref="INumberService.CreateNumberAsync"/>
        public async Task<int> CreateNumberAsync(CreateSingleNumberRequest createSingleNumberRequest, CancellationToken cancellationToken)
        {
            Guard.Against.Null(createSingleNumberRequest, nameof(createSingleNumberRequest));
            Guard.Against.NullOrEmpty(createSingleNumberRequest.Week, nameof(createSingleNumberRequest.Week));
            Guard.Against.Default(createSingleNumberRequest.RowId, nameof(createSingleNumberRequest.RowId));
            Guard.Against.Default(createSingleNumberRequest.PlayerId, nameof(createSingleNumberRequest.PlayerId));
            Guard.Against.Default(createSingleNumberRequest.Value, nameof(createSingleNumberRequest.Value));

            await using var connection = new SQLiteConnection(_configuration.ConnectionString);
            await connection.OpenAsync(cancellationToken);
            
            var number = new Number
            {
                Week = createSingleNumberRequest.Week,
                RowId = createSingleNumberRequest.RowId,
                PlayerId = createSingleNumberRequest.PlayerId,
                Type = createSingleNumberRequest.NumberType,
                Value = createSingleNumberRequest.Value,
            };

            var result = await connection.InsertAsync<Number>(number);
            return result.Value;
        }

        /// <inheritdoc cref="INumberService.CreateBulkNumberAsync"/>
        public async Task<int> CreateBulkNumberAsync(CreateBulkNumberForWeekRequest createBulkNumberForWeekRequest, CancellationToken cancellationToken)
        {
            Guard.Against.Null(createBulkNumberForWeekRequest, nameof(createBulkNumberForWeekRequest));

            await using var connection = new SQLiteConnection(_configuration.ConnectionString);
            await connection.OpenAsync(cancellationToken);

            var numbers = createBulkNumberForWeekRequest.Numbers.Select(x => new Number
            {
                RowId = x.RowId,
                PlayerId = x.PlayerId,
                Type = x.NumberType,
                Value = x.Value,
                Week = createBulkNumberForWeekRequest.Week,
            }).ToList();

            await connection.BulkActionAsync(op => op.BulkInsert(numbers), cancellationToken);
            return numbers.Count;
        }

        /// <inheritdoc cref="INumberService.UpdateNumbersForRowAsync"/>
        public async Task<int> UpdateNumbersForRowAsync(UpdateNumbersForRowAsync updateNumbersForRowAsync, CancellationToken cancellationToken)
        {
            Guard.Against.Null(updateNumbersForRowAsync, nameof(updateNumbersForRowAsync));

            await using var connection = new SQLiteConnection(_configuration.ConnectionString);
            await connection.OpenAsync(cancellationToken);
            
            var numbers = await connection.GetListAsync<Number>(new { Id = updateNumbersForRowAsync.Id});
            
            var numbersToAdd = new List<Number>();
            var numbersToUpdate = new List<Number>();
            foreach (var number in updateNumbersForRowAsync.Numbers)
            {
                var newNumber = numbers.FirstOrDefault(x => x.Id == number.Id);
                if (newNumber != null)
                {
                    numbersToUpdate.Add(number);
                }
                else
                {
                    numbersToAdd.Add(number);
                }
            }

            await connection.BulkActionAsync(op => op.BulkUpdate(numbersToUpdate), cancellationToken);
            await connection.BulkActionAsync(op => op.BulkInsert(numbersToAdd), cancellationToken);

            return numbersToUpdate.Count + numbersToAdd.Count;
        }
    }
}