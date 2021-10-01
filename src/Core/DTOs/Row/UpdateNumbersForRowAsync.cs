using System.Collections.Generic;
using System.Text.Json.Serialization;
using Core.DatabaseEntities;
using Microsoft.AspNetCore.Mvc;

namespace Core.DTOs.Row
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Part of DTO")]
    public class UpdateNumbersForRowAsync
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