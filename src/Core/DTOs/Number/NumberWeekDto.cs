using Core.Enums;

namespace Core.DTOs.Number
{
    public class NumberWeekDto
    {
        public int RowId { get; set; }

        public int NumberId { get; set; }

        public int PlayerId { get; set; }

        public NumberType Type { get; set; }

        public int Value { get; set; }
    }
}