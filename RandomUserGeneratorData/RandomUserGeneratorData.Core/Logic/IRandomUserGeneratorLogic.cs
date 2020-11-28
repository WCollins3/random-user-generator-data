using RandomUserGeneratorData.Core.Models;
using System.Threading.Tasks;

namespace RandomUserGeneratorData.Core.Logic
{
    /// <summary>
    ///     Interface for logic classes built to retrieve <see cref="RandomUserGeneratorDataHolder"/> objects.
    /// </summary>
    public interface IRandomUserGeneratorLogic
    {
        /// <summary>
        ///     Get a <see cref="RandomUserGeneratorDataHolder"/> with data on a number of <see cref="User"/>s.
        /// </summary>
        /// <param name="numUsers">Number of <see cref="User"/>s to get data for.</param>
        /// <returns></returns>
        Task<RandomUserGeneratorDataHolder> GetRandomUserGeneratorDataHolder(int numUsers);
    }
}
