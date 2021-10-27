using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Core.DTOs.Row;

namespace Core.Contracts
{
    public interface IRowService
    {
        Task<Result<int>> CreateRowAsync(CreateRowRequest createRowRequest, CancellationToken cancellationToken);
    }
}