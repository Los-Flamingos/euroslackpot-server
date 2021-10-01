using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Core.Contracts;
using Core.DTOs.Row;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.V1.Number
{
    public class GetAll : BaseAsyncEndpoint.WithoutRequest.WithResponse<GetAllRowResponse>
    {
        private readonly INumberService _numberService;

        public GetAll(INumberService numberService)
        {
            _numberService = numberService;
        }

        [HttpGet("v1/rows", Name = "GetAllRows")]
        [SwaggerOperation(
            Summary = "Get all rows",
            Description = "Get all rows",
            OperationId = "Number.GetAll",
            Tags = new[] { "Number" })]
        public override async Task<ActionResult<GetAllRowResponse>> HandleAsync(
            CancellationToken cancellationToken = new CancellationToken())
        {
            var rows = await _numberService.GetAllAsync(cancellationToken);
            return Ok(rows);
        }
    }
}
