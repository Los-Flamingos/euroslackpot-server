using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Core.Contracts;
using Core.DTOs.Number;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace API.V1.Number
{
    public class CreateNumber : BaseAsyncEndpoint.WithRequest<CreateNumberRequest>.WithResponse<int>
    {
        private readonly INumberService _numberService;
        private readonly ILogger<CreateNumber> _logger;

        public CreateNumber(INumberService numberService, ILogger<CreateNumber> logger)
        {
            _numberService = numberService;
            _logger = logger;
        }

        [HttpPost("v1/rows/{id}/numbers", Name = nameof(CreateNumber))]
        [SwaggerOperation(
            Summary = "Creates a new number",
            Description = "Creates a new number",
            OperationId = "Number.Create",
            Tags = new[] { "Number" })]
        public override async Task<ActionResult<int>> HandleAsync(
            [FromRoute] CreateNumberRequest createNumberRequest,
            CancellationToken cancellationToken = new ())
        {
            _logger.LogInformation("Player {PlayerId} creating new number {Number}", createNumberRequest.NumberRequest.PlayerId, createNumberRequest.NumberRequest.Value);

            var number = await _numberService.CreateNumberAsync(createNumberRequest, cancellationToken);

            return Ok(number);
        }
    }
}
