using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Core.Contracts;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.V1.Row
{
    public class Update : BaseAsyncEndpoint.WithRequest<UpdateRowRequest>.WithResponse<int>
    {
        private readonly IRowService _rowService;

        public Update(IRowService rowService)
        {
            _rowService = rowService;
        }

        [HttpPost("v1/rows", Name = "UpdateRow")]
        [SwaggerOperation(
            Summary = "Update row",
            Description = "Update row",
            OperationId = "Row.Update",
            Tags = new[] { "Row" })]
        public override async Task<ActionResult<int>> HandleAsync(
            [FromRoute] UpdateRowRequest request,
            CancellationToken cancellationToken = new CancellationToken())
        {
            int result = await _rowService.UpdateRowAsync(request, cancellationToken);
        }
    }

    public class UpdateRowRequest
    {
        [JsonPropertyName("id")]
        [FromRoute(Name = "id")]
        public int Id { get; set; }
        
        [JsonPropertyName("week")]
        [FromBody]
        public string Week { get; set; }

        [JsonPropertyName("numbers")]
        [FromBody]
        public ICollection<Number> Numbers { get; set; } = new List<Number>();
    }
}
