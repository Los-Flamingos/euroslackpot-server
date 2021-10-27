using System.Collections.Generic;
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
using Core.DTOs.Player;
using Core.Validators;
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

        public async Task<Result<IEnumerable<GetAllPlayersResponse>>> GetAllPlayersAsync(CancellationToken cancellationToken)
        {
            await using var connection = new SqlConnection(_configuration.ConnectionString);
            await connection.OpenAsync(cancellationToken);

            var result = await connection.GetAllAsync<Player>();

            return Result<IEnumerable<GetAllPlayersResponse>>.Success(result.Select(x => new GetAllPlayersResponse
            {
                Id = x.PlayerId,
                Name = x.PlayerName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
            }));
        }

        public async Task<Result<GetPlayerByIdResponse>> GetPlayerByIdAsync(int id, CancellationToken cancellationToken)
        {
            await using var connection = new SqlConnection(_configuration.ConnectionString);
            await connection.OpenAsync(cancellationToken);

            var result = await connection.GetAsync<Player>(id);
            if (result == null)
            {
                _logger.LogInformation("Player with id '{PlayerId}' was not found", id);
                return Result<GetPlayerByIdResponse>.NotFound();
            }

            return Result<GetPlayerByIdResponse>.Success(new GetPlayerByIdResponse
            {
                Id = result.PlayerId,
                Name = result.PlayerName,
                Email = result.Email,
                PhoneNumber = result.PhoneNumber,
            });
        }

        public async Task<Result<int>> CreatePlayerAsync(CreatePlayerRequest createPlayerRequest, CancellationToken cancellationToken)
        {
            Guard.Against.Null(createPlayerRequest, nameof(createPlayerRequest));

            var validator = new CreatePlayerRequestValidator();
            var validateResult = await validator.ValidateAsync(createPlayerRequest, cancellationToken);
            if (!validateResult.IsValid)
            {
                return Result<int>.Invalid(validateResult.AsErrors());
            }

            await using var connection = new SqlConnection(_configuration.ConnectionString);
            await connection.OpenAsync(cancellationToken);
            
            var player = new Player
            {
                PlayerName = createPlayerRequest.Name,
                PhoneNumber = createPlayerRequest.PhoneNumber,
                Email = createPlayerRequest.Email,
            };

            return await connection.InsertAsync(player);
        }
    }
}
