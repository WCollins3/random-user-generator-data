using RandomUserGeneratorData.Core.DataRetrieval;
using RandomUserGeneratorData.Core.Logic;
using RandomUserGeneratorData.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RandomUserGeneratorData.Logic
{
    public class RandomUserGeneratorLogic : IRandomUserGeneratorLogic
    {
        private IRandomUserRetriever _randomUserRetriever;

        public RandomUserGeneratorLogic(IRandomUserRetriever randomUserRetriever)
        {
            _randomUserRetriever = randomUserRetriever;
        }

        /// <inheritdoc/>
        public async Task<RandomUserGeneratorDataHolder> GetRandomUserGeneratorDataHolder(int numUsers)
        {
            var users = await _randomUserRetriever.GetUsersAsync(numUsers);
            return new RandomUserGeneratorDataHolder(users);
        }
    }
}
