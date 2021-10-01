using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.DTOs.Row;

namespace Core.Contracts
{
    public interface INumberService
    {
        /// <summary>
        /// GetNumbersForRowIdAsync gets all numbers for specific row
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>List of all numbers for row with id</returns>
        Task<GetNumbersForRowIdResponse> GetNumbersForRowIdAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// CreateNumberAsync creates a single number for a given week and row
        /// </summary>
        /// <param name="createSingleNumberRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Id of newly created number</returns>
        Task<int> CreateNumberAsync(CreateSingleNumberRequest createSingleNumberRequest, CancellationToken cancellationToken);

        /// <summary>
        /// CreateBulkNumberAsync creates a list of numbers, for a given week and row
        /// </summary>
        /// <param name="createBulkNumberRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Id of row the numbers were added to</returns>
        Task<int> CreateBulkNumberAsync(CreateBulkNumberForWeekRequest createBulkNumberRequest, CancellationToken cancellationToken);

        /// <summary>
        /// GetAllAsync gets all rows and numbers
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>List of rows with corresponding numbers</returns>
        Task<List<GetAllNumbersResponse>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// UpdateNumbersForRowAsync adds or updates numbers (depending if they exist or not already)
        /// </summary>
        /// <param name="updateNumbersForRowAsync"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Number of added/updated rows</returns>
        Task<int> UpdateNumbersForRowAsync(UpdateNumbersForRowAsync updateNumbersForRowAsync, CancellationToken cancellationToken);
    }
}