using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Core.Contracts;
using Core.DTOs.Row;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.V1.Row
{
    public class CreateRow : BaseAsyncEndpoint.WithRequest<CreateRowRequest>.WithResponse<int>
    {
        private readonly IRowService _rowService;

        public CreateRow(IRowService rowService)
        {
            _rowService = rowService;
        }

        [HttpPost("v1/rows", Name = "CreateRow")]
        [SwaggerOperation(
            Summary = "Create new row",
            Description = "Create new row",
            OperationId = "Row.Create",
            Tags = new[] { "Row" })]
        public override async Task<ActionResult<int>> HandleAsync(CreateRowRequest request, CancellationToken cancellationToken = new())
        {
            var result = await _rowService.CreateRowAsync(request, cancellationToken);
            return Ok(result);
        }
    }
}
