using System.Text.Json.Serialization;

namespace Core.DTOs.Player
{
    public class GetAllPlayersResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}