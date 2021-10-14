using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Core.Contracts;
using Core.DTOs.Row;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.V1.Row
{
    public class GetById : BaseAsyncEndpoint.WithRequest<int>.WithResponse<GetRowByIdResponse>
    {
        private readonly INumberService _numberService;

        public GetById(INumberService numberService)
        {
            _numberService = numberService;
        }

        [HttpGet("v1/rows/{id}", Name = "GetRowById")]
        [SwaggerOperation(
            Summary = "Get row by ID",
            Description = "Get row by ID",
            OperationId = "Number.GetById",
            Tags = new[] { "Number" })]
        public override async Task<ActionResult<GetRowByIdResponse>> HandleAsync(
            [FromRoute] int id,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var row = await _numberService.GetRowByIdAsync(id, cancellationToken);
            if (row == null)
            {
                return NotFound(new { id });
            }

            return Ok(row);
        }
    }
}
