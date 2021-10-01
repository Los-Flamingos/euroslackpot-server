using Core.Enums;

namespace Core.DTOs.Row
{
    public class GetNumberForRowIdNumberResponse
    {
        public int Id { get; set; }

        public int PlayerId { get; set; }

        public NumberType Type { get; set; }

        public int Value { get; set; }
    }
}