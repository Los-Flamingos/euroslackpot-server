using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Core.DTOs.Row
{
    public class CreateBulkNumberForWeekRequest
    {
        [JsonPropertyName("week")]
        public string Week { get; set; }

        [JsonPropertyName("numbers")]
        public ICollection<CreateBulkNumberRequest> Numbers { get; set; } = new List<CreateBulkNumberRequest>();
    }
}