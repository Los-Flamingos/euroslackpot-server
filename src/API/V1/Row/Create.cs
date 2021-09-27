using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Core.Contracts;
using Core.DTOs.Row;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.V1.Row
{
    public class Create : BaseAsyncEndpoint.WithRequest<CreateRowRequest>.WithResponse<CreateRowResponse>
    {
        private readonly IRowService _rowService;

        public Create(IRowService rowService)
        {
            _rowService = rowService;
        }

        [HttpPost("v1/rows", Name = "CreateRow")]
        [SwaggerOperation(
            Summary = "Create new row",
            Description = "Create new row",
            OperationId = "Row.Create",
            Tags = new[] { "Row" })]
        public override async Task<ActionResult<CreateRowResponse>> HandleAsync(
            [FromBody] CreateRowRequest createRowRequest,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var row = await _rowService.CreateRowAsync(createRowRequest, cancellationToken);

            return Ok(row);
        }
    }
}
