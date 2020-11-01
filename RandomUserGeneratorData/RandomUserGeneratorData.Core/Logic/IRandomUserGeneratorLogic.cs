using RandomUserGeneratorData.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RandomUserGeneratorData.Core.Logic
{
    public interface IRandomUserGeneratorLogic
    {
        /// <summary>
        ///     Get a <see cref="RandomUserGeneratorDataHolder"/> with data on a number of <see cref="User"/>s.
        /// </summary>
        /// <param name="numUsers">Number of <see cref="User"/>s to get data for.</param>
        /// <returns></returns>
        RandomUserGeneratorDataHolder GetRandomUserGeneratorDataHolder(int numUsers);
    }
}
