using System;
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
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Options;

namespace Data.Services
{
    public class RowService : IRowService
    {
        private readonly DatabaseConfigurationOptions _configuration;

        public RowService(IOptions<DatabaseConfigurationOptions> options)
        {
            Guard.Against.Null(options, nameof(options));
            Guard.Against.Null(options.Value, nameof(options.Value));

            _configuration = options.Value;
        }

        public async Task<GetRowByIdResponse> GetRowByIdAsync(int request, CancellationToken cancellationToken)
        {
            await using var connection = new SQLiteConnection(_configuration.ConnectionString);
            await connection.OpenAsync(cancellationToken);
            var result = await connection.GetAsync<Row>(request);
            return new GetRowByIdResponse
            {
                Id = result.RowId,
                Week = result.Week,
                CreatedAt = result.CreatedAt,
                Numbers = result.Numbers,
            };
        }

        public async Task<int> CreateRowAsync(CreateRowRequest createRowRequest, CancellationToken cancellationToken)
        {
            Guard.Against.Null(createRowRequest, nameof(createRowRequest));
            Guard.Against.NullOrEmpty(createRowRequest.Week, nameof(createRowRequest.Week));

            await using var connection = new SQLiteConnection(_configuration.ConnectionString);
            await connection.OpenAsync(cancellationToken);
            
            var row = new Row
            {
                Week = createRowRequest.Week,
                CreatedAt = DateTime.UtcNow,
                Numbers = createRowRequest.Numbers,
            };

            return await connection.InsertAsync(row);
        }

        public async Task<List<GetAllRowResponse>> GetAllAsync(CancellationToken cancellationToken)
        {
            await using var connection = new SQLiteConnection(_configuration.ConnectionString);
            await connection.OpenAsync(cancellationToken);
            var result = await connection.GetAllAsync<Row>();
            return result.Select(x => new GetAllRowResponse
            {
                Id = x.RowId,
                Week = x.Week,
                CreatedAt = x.CreatedAt,
                Numbers = x.Numbers,
            }).ToList();
        }
    }
}