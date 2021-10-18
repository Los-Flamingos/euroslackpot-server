using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Core.Contracts;
using Core.DTOs.Number;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Swashbuckle.AspNetCore.Annotations;

namespace API.V1.Number
{
    public class GetNumbersForWeek : BaseAsyncEndpoint.WithRequest<int>.WithResponse<GetNumbersForWeekResponse>
    {
        private readonly INumberService _numberService;
        private readonly ILogger<GetNumbersForWeek> _logger;

        public GetNumbersForWeek(INumberService numberService, ILogger<GetNumbersForWeek> logger)
        {
            _numberService = numberService;
            _logger = logger;
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
            _logger.LogInformation("Getting numbers for week {Week}", week);

            var rows = await _numberService.GetNumbersForWeekAsync(week, cancellationToken);
            
            return Ok(rows);
        }
    }
}
