﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Core.DatabaseEntities;

namespace Core.DTOs.Row
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Part of DTO")]
    public class GetAllRowResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("week")]
        public string Week { get; set; }

        [JsonPropertyName("numbers")]
        public ICollection<Number> Numbers { get; set; } = new List<Number>();
    }
}