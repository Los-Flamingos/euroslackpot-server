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
    public class GetById : BaseAsyncEndpoint
        .WithRequest<int>
        .WithResponse<GetPlayerByIdResponse>
    {
        private readonly IPlayerService playerService;
        private readonly ILogger<GetById> _logger;

        public GetById(IPlayerService playerService, ILogger<GetById> logger)
        {
            this.playerService = playerService;
            _logger = logger;
        }

        [HttpGet("v1/players/{id}", Name = "GetPlayerById")]
        [SwaggerOperation(
            Summary = "GetById player",
            Description = "GetById player",
            OperationId = "Player.GetById",
            Tags = new[] { "Player" })]
        public override async Task<ActionResult<GetPlayerByIdResponse>> HandleAsync(
            [FromRoute] int id,
            CancellationToken cancellationToken = new CancellationToken())
        {
            _logger.LogInformation("Player with id '{PlayerId}' was not found", id);

            var player = await playerService.GetPlayerByIdAsync(id, cancellationToken);
            if (player == null)
            {
                return NotFound(new { id });
            }

            return Ok(player);
        }
    }
}
