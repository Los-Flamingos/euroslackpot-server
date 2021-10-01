﻿using System.Text.Json.Serialization;
using Core.Enums;

namespace Core.DTOs.Row
{
    public class CreateSingleNumberRequest
    {
        [JsonPropertyName("week")]
        public string Week { get; set; }

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