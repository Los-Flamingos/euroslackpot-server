using Core.Enums;
using Dapper.Contrib.Extensions;
using Dapper.FluentMap.Mapping;

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

        public NumberType Type { get; set; }

        public int Value { get; set; }
    }

    public class NumberMap : EntityMap<Number>
    {
        public NumberMap()
        {
            Map(x => x.NumberId).ToColumn("NumberId");
            Map(x => x.RowId).ToColumn("RowId");
            Map(x => x.PlayerId).ToColumn("PlayerId");
            Map(x => x.Week).ToColumn("Week");
            Map(x => x.Type).ToColumn("NumberType");
            Map(x => x.Value).ToColumn("Value");
        }
    }
}
