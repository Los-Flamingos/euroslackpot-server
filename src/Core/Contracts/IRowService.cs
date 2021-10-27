using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Core.DTOs.Row;

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

        /// <summary>
        /// Update row by id
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Id of updated row</returns>
        Task<Result<int>> UpdateRowAsync(UpdateRowRequest request, CancellationToken cancellationToken);
    }
}