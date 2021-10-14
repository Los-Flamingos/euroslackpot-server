using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Core.Contracts;
using Core.DTOs.Number;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.V1.Number
{
    public class CreateNumber : BaseAsyncEndpoint.WithRequest<CreateNumberRequest>.WithResponse<int>
    {
        private readonly INumberService _numberService;

        public CreateNumber(INumberService numberService)
        {
            _numberService = numberService;
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
            var number = await _numberService.CreateNumberAsync(createNumberRequest, cancellationToken);
            return Ok(number);
        }
    }
}
