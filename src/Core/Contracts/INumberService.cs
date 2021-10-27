using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Core.DTOs.Number;

namespace Core.Contracts
{
    public interface INumberService
    {
        Task<Result<GetNumbersForWeekResponse>> GetNumbersForWeekAsync(int week, CancellationToken cancellationToken);

        Task<Result<int>> CreateNumberAsync(CreateNumberRequest createNumberRequest, CancellationToken cancellationToken);
    }
}