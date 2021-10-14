using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Core.DTOs.Number
{
    public class GetNumbersForRowResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("week")]
        public int Week { get; set; }

        [JsonPropertyName("numbers")]
        public ICollection<NumberRowDto> Numbers { get; set;} = new List<NumberRowDto>();
    }
}