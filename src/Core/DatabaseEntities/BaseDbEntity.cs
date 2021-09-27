using System;

namespace Core.DatabaseEntities
{
    public class BaseDbEntity
    {
        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }
    }
}