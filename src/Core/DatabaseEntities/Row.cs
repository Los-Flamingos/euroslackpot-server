using Dapper.Contrib.Extensions;
using Dapper.FluentMap.Mapping;

namespace Core.DatabaseEntities
{
    [Table("Row")]
    public class Row
    {
        [Key]
        public int Id { get; set; }

        public int Week { get; set; }

        public double Earnings { get; set; }
    }

    public class RowMap : EntityMap<Row>
    {
        public RowMap()
        {
            Map(x => x.Id).ToColumn("RowId");
            Map(x => x.Week).ToColumn("Week");
            Map(x => x.Earnings).ToColumn("Earnings");
        }
    }
}