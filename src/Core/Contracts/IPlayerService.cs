using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.DTOs.Player;

namespace Core.Contracts
{
    public interface IPlayerService
    {
        Task<IEnumerable<GetAllPlayersResponse>> GetAllPlayersAsync(CancellationToken cancellationToken);

        Task<GetPlayerByIdResponse> GetPlayerByIdAsync(int id, CancellationToken cancellationToken);
        
        Task<int> CreatePlayerAsync(CreatePlayerRequest request, CancellationToken cancellationToken);
    }
}
