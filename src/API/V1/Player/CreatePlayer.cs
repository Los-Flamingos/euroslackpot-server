using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Core.Contracts;
using Core.DTOs.Player;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace API.V1.Player
{
    public class CreatePlayer : BaseAsyncEndpoint.WithRequest<CreatePlayerRequest>.WithResponse<int>
    {
        private readonly IPlayerService _playerService;
        private readonly ILogger<CreatePlayer> _logger;

        public CreatePlayer(IPlayerService playerService, ILogger<CreatePlayer> logger)
        {
            _playerService = playerService;
            _logger = logger;
        }

        [HttpPost("v1/players", Name = "CreatePlayer")]
        [SwaggerOperation(
            Summary = "Create new player",
            Description = "Create new player",
            OperationId = "Player.Create",
            Tags = new[] { "Player" })]
        public override async Task<ActionResult<int>> HandleAsync(
            [FromBody] CreatePlayerRequest request,
            CancellationToken cancellationToken = new CancellationToken())
        {
            _logger.LogInformation("Creating new player");

            var player = await _playerService.CreatePlayerAsync(request, cancellationToken);

            return Ok(player);
        }
    }
}
