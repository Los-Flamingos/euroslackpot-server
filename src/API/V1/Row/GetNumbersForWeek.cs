using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Core.Contracts;
using Core.DTOs.Row;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.V1.Row
{
    public class GetNumbersForWeek : BaseAsyncEndpoint.WithoutRequest.WithResponse<GetNumbersForWeekResponse>
    {
        private readonly INumberService _numberService;

        public GetNumbersForWeek(INumberService numberService)
        {
            _numberService = numberService;
        }

        [HttpGet("v1/numbers", Name = nameof(GetNumbersForWeek))]
        [SwaggerOperation(
            Summary = "Get all numbers for specific week",
            Description = "Get all numbers for specific week",
            OperationId = "Number.GetNumbersForWeek",
            Tags = new[] { "Number" })]
        public override async Task<ActionResult<GetNumbersForWeekResponse>> HandleAsync(CancellationToken cancellationToken = new())
        {
            var rows = await _numberService.GetNumbersForWeekAsync(cancellationToken);
            return Ok(rows);
        }
    }
}
