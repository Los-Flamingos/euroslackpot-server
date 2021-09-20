using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Contracts
{
    public interface IPlayerService
    {
        Task<IEnumerable<Player>> GetAllPlayersAsync(CancellationToken cancellationToken);

        Task<Player> GetPlayerByIdAsync(int id, CancellationToken cancellationToken);
    }
}
