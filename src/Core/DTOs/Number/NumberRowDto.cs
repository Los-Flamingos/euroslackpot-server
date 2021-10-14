using Core.Enums;

namespace Core.DTOs.Number
{
    public class NumberRowDto
    {
        public int NumberId { get; set; }

        public int PlayerId { get; set; }

        public NumberType Type { get; set; }

        public int Value { get; set; }
    }
}