using System.Text.Json.Serialization;

namespace Core.DTOs.Player
{
    public class CreatePlayerRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}