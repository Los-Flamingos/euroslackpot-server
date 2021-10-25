using Dapper.Contrib.Extensions;

namespace Core.DatabaseEntities
{
    [Table("Player")]
    public class Player
    {
        [Key]
        public int PlayerId { get; set; }

        public string PlayerName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}