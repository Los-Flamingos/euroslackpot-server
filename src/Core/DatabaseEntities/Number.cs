using Core.Enums;
using Dapper;

namespace Core.DatabaseEntities
{
    [Table("number")]
    public class Number : BaseDbEntity
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Week")]
        public string Week { get; set; }

        [Column("RowId")]
        public int RowId { get; set; }

        [Column("PlayerId")]
        public int PlayerId { get; set; }

        [Column("Type")]
        public NumberType Type { get; set; }

        [Column("Value")]
        public int Value { get; set; }
    }
}
