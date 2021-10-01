using System;
using Dapper;

namespace Core.DatabaseEntities
{
    public class BaseDbEntity
    {
        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [Column("ModifiedAt")]
        public DateTime ModifiedAt { get; set; }
    }
}