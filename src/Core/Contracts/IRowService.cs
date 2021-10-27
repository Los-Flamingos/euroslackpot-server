using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;

namespace Core.Contracts
{
    public interface IRowService
    {
        /// <summary>
        /// CreateRowAsync creates a new row
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Database id of newly created row</returns>
        Task<Result<int>> CreateRowAsync(CancellationToken cancellationToken);
    }
}