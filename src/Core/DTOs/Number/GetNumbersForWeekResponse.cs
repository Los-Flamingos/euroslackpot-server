using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Core.DTOs.Number
{
    public class GetNumbersForWeekResponse
    {
        [JsonPropertyName("week")]
        public int Week { get; set; }

        [JsonPropertyName("numbers")]
        public ICollection<NumberWeekDto> Numbers { get; set; } = new List<NumberWeekDto>();
    }
}