using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.DTOs.Row;

namespace Core.Contracts
{
    public interface INumberService
    {
        Task<GetRowByIdResponse> GetRowByIdAsync(int request, CancellationToken cancellationToken);

        Task<int> CreateRowAsync(CreateRowRequest createRowRequest, CancellationToken cancellationToken);

        Task<List<GetNumbersForWeekResponse>> GetNumbersForWeekAsync(CancellationToken cancellationToken);
    }
}