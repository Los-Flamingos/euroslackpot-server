using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Core.Contracts;
using Core.DTOs.Row;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.V1.Number
{
    public class Update : BaseAsyncEndpoint.WithRequest<UpdateNumbersForRowAsync>.WithResponse<int>
    {
        private readonly INumberService _numberService;

        public Update(INumberService numberService)
        {
            _numberService = numberService;
        }

        [HttpPut("v1/rows", Name = "UpdateRow")]
        [SwaggerOperation(
            Summary = "Update row",
            Description = "Update row",
            OperationId = "Number.Update",
            Tags = new[] { "Number" })]
        public override async Task<ActionResult<int>> HandleAsync(
            [FromRoute] UpdateNumbersForRowAsync @async,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await _numberService.UpdateNumbersForRowAsync(async, cancellationToken);
            return Ok(result);
        }
    }
}
