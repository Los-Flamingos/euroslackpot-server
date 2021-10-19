using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Common.Exceptions;
using Common.Helpers;
using Core.ConfigurationOptions;
using Core.Contracts;
using Core.DatabaseEntities;
using Core.DTOs.Player;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Data.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly DatabaseConfigurationOptions _configuration;
        private readonly ILogger<PlayerService> _logger;

        public PlayerService(IOptions<DatabaseConfigurationOptions> options, ILogger<PlayerService> logger)
        {
            Guard.Against.Null(options, nameof(options));
            Guard.Against.Null(options.Value, nameof(options.Value));

            _logger = logger;
            _configuration = options.Value;
        }

        public async Task<IEnumerable<GetAllPlayersResponse>> GetAllPlayersAsync(CancellationToken cancellationToken)
        {
            await using var connection = new SqlConnection(_configuration.ConnectionString);
            await connection.OpenAsync(cancellationToken);

            var result = await connection.GetAllAsync<Player>();

            return result.Select(x => new GetAllPlayersResponse
            {
                Id = x.PlayerId,
                Name = x.Name,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
            });
        }

        public async Task<GetPlayerByIdResponse> GetPlayerByIdAsync(int id, CancellationToken cancellationToken)
        {
            await using var connection = new SqlConnection(_configuration.ConnectionString);
            await connection.OpenAsync(cancellationToken);

            var result = await connection.GetAsync<Player>(id);
            if (result == null)
            {
                _logger.LogInformation("Player with id '{PlayerId}' was not found", id);
                return null;
            }

            return new GetPlayerByIdResponse
            {
                Id = result.PlayerId,
                Name = result.Name,
                Email = result.Email,
                PhoneNumber = result.PhoneNumber,
            };
        }

        public async Task<int> CreatePlayerAsync(CreatePlayerRequest createPlayerRequest, CancellationToken cancellationToken)
        {
            Guard.Against.Null(createPlayerRequest, nameof(createPlayerRequest));
            Guard.Against.NullOrEmpty(createPlayerRequest.Name, nameof(createPlayerRequest.Name));

            // TODO Consider validate format of email as well. Refactor validators to own methods
            if (!PhoneNumberHelper.IsValidSwedishPhoneNumber(createPlayerRequest.PhoneNumber))
            {
                var exception = new InvalidPhoneNumberException($"Invalid format of Swedish phone number value '{createPlayerRequest.PhoneNumber}");
                _logger.LogError(exception, exception.Message);
                throw exception;
            }

            await using var connection = new SqlConnection(_configuration.ConnectionString);
            await connection.OpenAsync(cancellationToken);
            
            var player = new Player
            {
                Name = createPlayerRequest.Name,
                PhoneNumber = createPlayerRequest.PhoneNumber,
                Email = createPlayerRequest.Email,
            };

            return await connection.InsertAsync(player);
        }
    }
}
