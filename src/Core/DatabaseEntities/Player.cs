using Dapper;

namespace Core.DatabaseEntities
{
    [Table("player")]
    public class Player : BaseDbEntity
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }
    }
}