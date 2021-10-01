using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Core.DTOs.Row
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Part of DTO")]
    public class GetAllNumbersResponse
    {
        [JsonPropertyName("week")]
        public string Week { get; set; }

        [JsonPropertyName("numbers")]
        public ICollection<GetNumbersForAllNumbersResponse> Numbers { get; set;} = new List<GetNumbersForAllNumbersResponse>();
    }
}