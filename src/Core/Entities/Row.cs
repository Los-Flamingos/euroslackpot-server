using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class Row
    {
        public DateTime CreatedAt { get; set; }

        public string Week { get; set; }

        public ICollection<Number> Numbers { get; set; }
    }
}
