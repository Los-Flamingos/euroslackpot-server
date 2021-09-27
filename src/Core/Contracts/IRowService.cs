using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.DTOs.Row;

namespace Core.Contracts
{
    public interface IRowService
    {
        Task<GetRowByIdResponse> GetRowByIdAsync(int request, CancellationToken cancellationToken);

        Task<int> CreateRowAsync(CreateRowRequest createRowRequest, CancellationToken cancellationToken);

        Task<List<GetAllRowResponse>> GetAllAsync(CancellationToken cancellationToken);

        Task<int> UpdateRowAsync(UpdateRowRequest request, CancellationToken cancellationToken);
    }
}