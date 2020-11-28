using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RandomUserGeneratorData.Core.DataRetrieval;
using RandomUserGeneratorData.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RandomUserGeneratorData.Logic.Tests
{
    /// <summary>
    ///     Tests for the <see cref="RandomUserGeneratorLogic"/> class.
    /// </summary>
    [TestClass]
    public class RandomUserGeneratorLogicTests
    {
        /// <summary>
        ///     Tests the basic functionality of the GetRandomUserGeneratorDataHolder method receiving
        ///     a positive number and returning a <see cref="RandomUserGeneratorDataHolder"/> object.
        /// </summary>
        /// <returns>N/A</returns>
        [TestMethod]
        public async Task GetRandomUserGeneratorDataHolder_Valid()
        {
            int numUsers = 1;

            var randomUserRetriever = new Mock<IRandomUserRetriever>(MockBehavior.Strict);

            var userList = new List<User>
            {
                new User
                {
                    Gender = "Male",
                    FirstName = "Jon",
                    LastName = "Snow",
                    Country = "House Stark"
                }
            };

            randomUserRetriever.Setup(x => x.GetUsersAsync(numUsers))
                .ReturnsAsync(userList);

            var logic = new RandomUserGeneratorLogic(randomUserRetriever.Object);

            var result = await logic.GetRandomUserGeneratorDataHolder(numUsers);

            // Only checking that it is not null because the actual logic to
            // populate the values in the result are done in the object's constructor.
            Assert.IsNotNull(result);
            randomUserRetriever.VerifyAll();
        }

        /// <summary>
        ///     Tests that the GetRandomUserGeneratorDataHolder method throws an
        ///     <see cref="InvalidOperationException"/> when the numUsers parameter is 0.
        /// </summary>
        /// <returns>N/A</returns>
        [TestMethod]
        public async Task GetRandomUserGeneratorDataHolder_Zero_Invalid()
        {
            int numUsers = 0;

            var randomUserRetriever = new Mock<IRandomUserRetriever>(MockBehavior.Strict);

            var logic = new RandomUserGeneratorLogic(randomUserRetriever.Object);

            var ex = await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => logic.GetRandomUserGeneratorDataHolder(numUsers));
            Assert.AreEqual($"numUsers must be at least 1. Value of numUsers: {numUsers}.", ex.Message);
            randomUserRetriever.VerifyAll();
        }

        /// <summary>
        ///     Tests that the GetRandomUserGeneratorDataHolder method throws an
        ///     <see cref="InvalidOperationException"/> when the numUsers parameter is negative.
        /// </summary>
        /// <returns>N/A</returns>
        [TestMethod]
        public async Task GetRandomUserGeneratorDataHolder_Negative_Invalid()
        {
            int numUsers = -1;

            var randomUserRetriever = new Mock<IRandomUserRetriever>(MockBehavior.Strict);

            var logic = new RandomUserGeneratorLogic(randomUserRetriever.Object);

            var ex = await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => logic.GetRandomUserGeneratorDataHolder(numUsers));
            Assert.AreEqual($"numUsers must be at least 1. Value of numUsers: {numUsers}.", ex.Message);
            randomUserRetriever.VerifyAll();
        }
    }
}
