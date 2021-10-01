using Core.Enums;

namespace Core.DTOs.Row
{
    public class GetNumbersForAllNumbersResponse
    {
        public int Id { get; set; }

        public int RowId { get; set; }

        public int PlayerId { get; set; }

        public NumberType Type { get; set; }

        public int Value { get; set; }
    }
}