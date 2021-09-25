using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Core.Entities;

namespace Core.DTOs.Row
{
    public class GetRowByIdResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("week")]
        public string Week { get; set; }

        [JsonPropertyName("numbers")]
        public ICollection<Number> Numbers { get; set;} = new List<Number>();
    }
}