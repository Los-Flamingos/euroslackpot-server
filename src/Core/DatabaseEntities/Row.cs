using Dapper.Contrib.Extensions;

namespace Core.DatabaseEntities
{
    [Table("Row")]
    public class Row
    {
        [Key]
        public int RowId { get; set; }

        public decimal Earnings { get; set; }
    }
}