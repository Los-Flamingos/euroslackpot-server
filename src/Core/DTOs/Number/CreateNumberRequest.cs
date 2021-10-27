using Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Core.DTOs.Number
{

    public class CreateNumberRequest
    {
        [FromRoute(Name = "id")]
        public int RowId { get; set; }

        [FromBody]
        public NumberRequest NumberRequest { get; set; }
    }

    public class NumberRequest
    {
        public int PlayerId { get; set; }
        
        public int Week { get; set; }
        
        public NumberType Type { get; set; }
        
        public int Value { get; set; }
    }
}