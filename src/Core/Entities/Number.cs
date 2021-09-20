using Core.Enums;

namespace Core.Entities
{
    public class Number
    {
        public int Value { get; set; }

        public NumberType NumberType { get; set; }

        public Player Player { get; set; }
    }
}