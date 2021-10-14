using Dapper.Contrib.Extensions;
using Dapper.FluentMap.Mapping;

namespace Core.DatabaseEntities
{
    [Table("Player")]
    public class Player
    {
        [Key]
        public int PlayerId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }

    public class PlayerMap : EntityMap<Player>
    {
        public PlayerMap()
        {
            Map(x => x.PlayerId).ToColumn("PlayerId");
            Map(x => x.Name).ToColumn("PlayerName");
            Map(x => x.Email).ToColumn("Email");
            Map(x => x.PhoneNumber).ToColumn("PhoneNumber");
        }
    }
}