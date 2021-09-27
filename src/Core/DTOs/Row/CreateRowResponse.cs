using System;
using System.Collections.Generic;
using Core.Entities;

namespace Core.DTOs.Row
{
    public class CreateRowResponse
    {
        public DateTime CreatedAt { get; set; }

        public string Week { get; set; }

        public ICollection<Number> Numbers { get; set; } = new List<Number>();
    }
}