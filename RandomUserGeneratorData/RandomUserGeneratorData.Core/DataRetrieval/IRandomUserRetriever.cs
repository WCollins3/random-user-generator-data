using RandomUserGeneratorData.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RandomUserGeneratorData.Core.DataRetrieval
{
    public interface IRandomUserRetriever
    {
        /// <summary>
        ///     Get <see cref="User"/>s from data source.
        /// </summary>
        /// <param name="numUsers">Number of <see cref="User"/>s to get.</param>
        /// <returns></returns>
        Task<IList<User>> GetUsersAsync(int numUsers);
    }
}
