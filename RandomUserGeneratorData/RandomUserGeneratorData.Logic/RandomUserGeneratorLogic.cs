using RandomUserGeneratorData.Core.DataRetrieval;
using RandomUserGeneratorData.Core.Logic;
using RandomUserGeneratorData.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
        public RandomUserGeneratorDataHolder GetRandomUserGeneratorDataHolder(int numUsers)
        {
            var users = _randomUserRetriever.GetUsers(numUsers);
            return new RandomUserGeneratorDataHolder(users);
        }
    }
}
