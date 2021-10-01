using System.Text.Json.Serialization;
using Core.Enums;

namespace Core.DTOs.Row
{
    public class CreateRowNumberRequest
    {
        [JsonPropertyName("playerId")]
        public int PlayerId { get; set; }

        [JsonPropertyName("rowId")]
        public int RowId { get; set; }

        [JsonPropertyName("value")]
        public int Value { get; set; }

        [JsonPropertyName("numberType")]
        public NumberType NumberType { get; set; }
    }
}