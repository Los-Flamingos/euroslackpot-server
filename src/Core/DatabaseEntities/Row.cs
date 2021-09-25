using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities;

namespace Core.DatabaseEntities
{
    [Table("row")]
    public class Row : BaseDbEntity
    {
        [Key]
        public int RowId { get; set; }

        public string Week { get; set; }

        public ICollection<Number> Numbers { get; set; }
    }
}
