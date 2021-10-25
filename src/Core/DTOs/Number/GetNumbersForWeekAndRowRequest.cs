using Microsoft.AspNetCore.Mvc;

namespace Core.DTOs.Number
{
    public class GetNumbersForWeekAndRowRequest
    {
        [FromRoute(Name = "week")]
        public int Week { get; set; }

        [FromRoute(Name = "rowId")]
        public int RowId { get; set; }
    }
}