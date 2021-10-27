using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Core.DTOs.Row
{
    public class UpdateRowRequest
    {
        [FromRoute(Name = "id")] public int Id { get; set; }
        
        [FromBody] public UpdateRow UpdateRow { get; set; }
    }

    public class UpdateRow
    {
        [JsonPropertyName("earnings")]
        public decimal Earnings { get; set; }
    }
}
