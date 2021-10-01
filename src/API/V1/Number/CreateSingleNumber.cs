using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Core.Contracts;
using Core.DTOs.Row;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.V1.Number
{
    public class CreateSingleNumber : BaseAsyncEndpoint.WithRequest<CreateSingleNumberRequest>.WithResponse<int>
    {
        private readonly INumberService _numberService;

        public CreateSingleNumber(INumberService numberService)
        {
            _numberService = numberService;
        }

        [HttpPost("v1/rows", Name = "CreateRow")]
        [SwaggerOperation(
            Summary = "CreateSingleNumber new row",
            Description = "CreateSingleNumber new row",
            OperationId = "Number.CreateSingleNumber",
            Tags = new[] { "Number" })]
        public override async Task<ActionResult<int>> HandleAsync(
            [FromBody] CreateSingleNumberRequest createSingleNumberRequest,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var row = await _numberService.CreateNumberAsync(createSingleNumberRequest, cancellationToken);
            return Ok(row);
        }
    }
}
