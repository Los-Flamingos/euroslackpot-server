using System.Collections.Generic;
using Core.Entities;

namespace Core.DTOs.Row
{
    public class CreateRowRequest
    {
        public string Week { get; set; }

        public ICollection<Number> Numbers { get; set; } = new List<Number>();
    }
}