using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Core.Contracts;
using Core.DTOs.Number;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.V1.Number
{
    public class GetNumbersForRow : BaseAsyncEndpoint.WithRequest<int>.WithResponse<GetNumbersForRowResponse>
    {
        private readonly INumberService _numberService;

        public GetNumbersForRow(INumberService numberService)
        {
            _numberService = numberService;
        }

        [HttpGet("v1/numbers/{rowId}", Name = nameof(GetNumbersForRow))]
        [SwaggerOperation(
            Summary = "Get numbers for row",
            Description = "Get numbers for row",
            OperationId = "Number.GetNumbersForRow",
            Tags = new[] { "Number" })]
        public override async Task<ActionResult<GetNumbersForRowResponse>> HandleAsync(
            [FromRoute] int rowId,
            CancellationToken cancellationToken = new ())
        {
            var row = await _numberService.GetNumbersForRowAsync(rowId, cancellationToken);
            if (row == null)
            {
                return NotFound(new { rowId });
            }

            return Ok(row);
        }
    }
}
