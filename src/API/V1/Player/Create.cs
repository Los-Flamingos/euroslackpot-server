using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Core.Contracts;
using Core.DTOs.Player;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.V1.Player
{
    public class Create
        : BaseAsyncEndpoint.WithRequest<CreatePlayerRequest>.WithResponse<int>
    {
        private readonly IPlayerService _playerService;

        public Create(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpPost("v1/players", Name = "CreatePlayer")]
        [SwaggerOperation(
            Summary = "CreateSingleNumber new player",
            Description = "CreateSingleNumber new player",
            OperationId = "Player.CreateSingleNumber",
            Tags = new[] { "Player" })]
        public override async Task<ActionResult<int>> HandleAsync(
            [FromBody] CreatePlayerRequest request,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var player = await _playerService.CreatePlayerAsync(request, cancellationToken);
            return Ok(player);
        }
    }
}
