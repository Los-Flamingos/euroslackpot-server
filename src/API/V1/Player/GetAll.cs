using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Ardalis.ApiEndpoints;

using Core.Contracts;
using Core.DTOs.Player;

using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace API.V1.Player
{
    public class GetAll : BaseAsyncEndpoint.WithoutRequest.WithResponse<IEnumerable<GetAllPlayersResponse>>
    {
        private readonly IPlayerService _playerService;

        public GetAll(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet("v1/players", Name = "GetAllPlayers")]
        [SwaggerOperation(
            Summary = "Get all players",
            Description = "Get all players",
            OperationId = "Player.GetAll",
            Tags = new[] { "Player" })]
        public override async Task<ActionResult<IEnumerable<GetAllPlayersResponse>>> HandleAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var players = await _playerService.GetAllPlayersAsync(cancellationToken);
            return Ok(players);
        }
    }
}
