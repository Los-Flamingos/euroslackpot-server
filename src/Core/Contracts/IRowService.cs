using System.Threading;
using System.Threading.Tasks;
using Core.DTOs.Row;

namespace Core.Contracts
{
    public interface IRowService
    {
        Task<int> CreateRowAsync(CreateRowRequest createRowRequest, CancellationToken cancellationToken);
    }
}