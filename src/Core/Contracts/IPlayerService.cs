using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Core.DTOs.Player;

namespace Core.Contracts
{
    public interface IPlayerService
    {
        Task<Result<IEnumerable<GetAllPlayersResponse>>> GetAllPlayersAsync(CancellationToken cancellationToken);

        Task<Result<GetPlayerByIdResponse>> GetPlayerByIdAsync(int id, CancellationToken cancellationToken);
        
        Task<Result<int>> CreatePlayerAsync(CreatePlayerRequest request, CancellationToken cancellationToken);
    }
}
