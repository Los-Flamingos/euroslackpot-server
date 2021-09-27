using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.DatabaseEntities
{
    [Table("player")]
    public class Player : BaseDbEntity
    {
        [Key]
        public int PlayerId { get; set; }

        public string Name { get; set; }
    }
}