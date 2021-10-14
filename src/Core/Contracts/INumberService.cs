using System.Threading;
using System.Threading.Tasks;
using Core.DTOs.Number;

namespace Core.Contracts
{
    public interface INumberService
    {
        Task<GetNumbersForRowResponse> GetNumbersForRowAsync(int rowId, CancellationToken cancellationToken);

        Task<GetNumbersForWeekResponse> GetNumbersForWeekAsync(int week, CancellationToken cancellationToken);

        Task<int> CreateNumberAsync(CreateNumberRequest createNumberRequest, CancellationToken cancellationToken);
    }
}