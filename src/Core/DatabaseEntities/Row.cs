using Dapper.Contrib.Extensions;

namespace Core.DatabaseEntities
{
    [Table("Row")]
    public class Row
    {
        [Key]
        public int RowId { get; set; }

        public int Week { get; set; }

        public double Earnings { get; set; }
    }
}