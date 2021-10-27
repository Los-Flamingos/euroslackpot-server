using System.Threading;
using System.Threading.Tasks;
using API.V1.Number;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using Core.Contracts;
using Core.DTOs.Number;
using Core.DTOs.Row;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.V1.Row
{
    public class UpdateRow : BaseAsyncEndpoint.WithRequest<UpdateRowRequest>.WithResponse<int>
    {
        private readonly IRowService _rowService;

        public UpdateRow(IRowService rowService) => _rowService = rowService;
        
        [HttpPut("v1/rows/{id}", Name = nameof(UpdateRow))]
        [SwaggerOperation(
            Summary = "Update row",
            Description = "Update row",
            OperationId = "Row.Update",
            Tags = new[] { "Row" })]
        public override async Task<ActionResult<int>> HandleAsync(
            [FromRoute] UpdateRowRequest request,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await _rowService.UpdateRowAsync(request, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}
