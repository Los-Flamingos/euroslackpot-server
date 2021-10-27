using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Core.Contracts;
using Core.DTOs.Number;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.V1.Number
{
    public class GetNumbersForWeek : BaseAsyncEndpoint.WithRequest<int>.WithResponse<GetNumbersForWeekResponse>
    {
        private readonly INumberService _numberService;

        public GetNumbersForWeek(INumberService numberService)
        {
            _numberService = numberService;
        }

        [HttpGet("v1/numbers/{week}", Name = nameof(GetNumbersForWeek))]
        [SwaggerOperation(
            Summary = "Get all numbers for specific week",
            Description = "Get all numbers for specific week",
            OperationId = "Number.GetNumbersForWeek",
            Tags = new[] { "Number" })]
        public override async Task<ActionResult<GetNumbersForWeekResponse>> HandleAsync(
            [FromRoute] int week,
            CancellationToken cancellationToken = new())
        {
            return this.ToActionResult(await _numberService.GetNumbersForWeekAsync(week, cancellationToken));
        }
    }
}
