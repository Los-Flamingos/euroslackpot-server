using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.V1.Player
{
    public class GetAll : BaseAsyncEndpoint.WithoutRequest.WithResponse<IEnumerable<GetPlayerResponse>>
    {
        private readonly IPlayerService _playerService;

        public GetAll(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        [Route("v1/players")]
        [SwaggerOperation(
            Summary = "Get all players",
            Description = "Get all players",
            OperationId = "Player.GetAll",
            Tags = new[] { "Player" })]
        public override async Task<ActionResult<IEnumerable<GetPlayerResponse>>> HandleAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var player = await _playerService.GetAllPlayersAsync(cancellationToken);
            return Ok(player.Select(x => new GetPlayerResponse { Id = x.Id, Name = x.Name } ));
        }
    }
    public class Get : BaseAsyncEndpoint
        .WithRequest<int>
        .WithResponse<GetPlayerResponse>
    {
        private readonly IPlayerService _playerService;

        public Get(IPlayerService playerService)
        {
            _playerService = playerService;
        }
        
        [HttpGet]
        [Route("v1/players/{id}")]
        [SwaggerOperation(
            Summary = "Get player",
            Description = "Get player",
            OperationId = "Player.Get",
            Tags = new[] { "Player" })]
        public override async Task<ActionResult<GetPlayerResponse>> HandleAsync(int id, CancellationToken cancellationToken = new CancellationToken())
        {
            var player = await _playerService.GetPlayerByIdAsync(id, cancellationToken);
            if (player == null)
            {
                return NotFound();
            }

            return Ok(new GetPlayerResponse { Id = player.Id, Name = player.Name } );
        }
    }

    public class GetPlayerResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
