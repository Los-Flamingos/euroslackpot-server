using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Core.DTOs.Row
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Part of DTO")]
    public class GetNumbersForRowIdResponse
    {
        [JsonPropertyName("week")]
        public string Week { get; set; }

        [JsonPropertyName("rowId")]
        public int RowId { get; set; }

        [JsonPropertyName("numbers")]
        public ICollection<GetNumberForRowIdNumberResponse> Numbers { get; set;} = new List<GetNumberForRowIdNumberResponse>();
    }
}