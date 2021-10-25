using Core.Enums;
using Dapper.Contrib.Extensions;

namespace Core.DatabaseEntities
{
    [Table("Number")]
    public class Number
    {
        [Key]
        public int NumberId { get; set; }

        public int RowId { get; set; }

        public int PlayerId { get; set; }

        public int Week { get; set; }

        public NumberType NumberType { get; set; }

        public int Value { get; set; }
    }
}
