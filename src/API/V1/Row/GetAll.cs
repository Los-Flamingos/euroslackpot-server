using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Core.Contracts;
using Core.DTOs.Row;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.V1.Row
{
    public class GetAll : BaseAsyncEndpoint.WithoutRequest.WithResponse<GetAllRowResponse>
    {
        private readonly IRowService _rowService;

        public GetAll(IRowService rowService)
        {
            _rowService = rowService;
        }

        [HttpGet("v1/rows", Name = "GetAllRows")]
        [SwaggerOperation(
            Summary = "Get all rows",
            Description = "Get all rows",
            OperationId = "Row.GetAll",
            Tags = new[] { "Row" })]
        public override async Task<ActionResult<GetAllRowResponse>> HandleAsync(
            CancellationToken cancellationToken = new CancellationToken())
        {
            var rows = await _rowService.GetAllAsync(cancellationToken);
            return Ok(rows);
        }
    }
}
