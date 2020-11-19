using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RandomUserGeneratorData.Controllers;
using RandomUserGeneratorData.Core.Logic;
using RandomUserGeneratorData.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RandomUserGeneratorData.Tests.Controllers
{
    /// <summary>
    ///     Tests for the <see cref="RandomUserGeneratorDataController"/> class.
    /// </summary>
    [TestClass]
    public class RandomUserGeneratorDataControllerTests
    {
        /// <summary>
        ///     Tests the basic functionality of the Get method receiving a positive number and
        ///     returning an <see cref="OkObjectResult"/>.
        /// </summary>
        /// <returns>N/A</returns>
        [TestMethod]
        public async Task Get_Valid()
        {
            int numUsers = 1;

            var logger = new Mock<ILogger<RandomUserGeneratorDataController>>(MockBehavior.Strict);
            var logic = new Mock<IRandomUserGeneratorLogic>(MockBehavior.Strict);

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

            logic.Setup(x => x.GetRandomUserGeneratorDataHolder(numUsers))
                .ReturnsAsync(new RandomUserGeneratorDataHolder(userList));

            var controller = new RandomUserGeneratorDataController(logger.Object, logic.Object);

            var result = await controller.Get(numUsers);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            logic.VerifyAll();
            logger.VerifyAll();
        }

        /// <summary>
        ///     Tests that the Get method returns a <see cref="BadRequestObjectResult"/> when 0
        ///     <see cref="User"/>s are requested.
        /// </summary>
        /// <returns>N/A</returns>
        [TestMethod]
        public async Task Get_Zero_Invalid()
        {
            int numUsers = 0;

            var logger = new Mock<ILogger<RandomUserGeneratorDataController>>(MockBehavior.Strict);
            var logic = new Mock<IRandomUserGeneratorLogic>(MockBehavior.Strict);

            var controller = new RandomUserGeneratorDataController(logger.Object, logic.Object);

            var result = await controller.Get(numUsers);

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            logic.VerifyAll();
            logger.VerifyAll();
        }

        /// <summary>
        ///     Tests that the Get method returns a <see cref="BadRequestObjectResult"/> when -1
        ///     <see cref="User"/>s are requested.
        /// </summary>
        /// <returns>N/A</returns>
        [TestMethod]
        public async Task Get_Negative_Invalid()
        {
            int numUsers = -1;

            var logger = new Mock<ILogger<RandomUserGeneratorDataController>>(MockBehavior.Strict);
            var logic = new Mock<IRandomUserGeneratorLogic>(MockBehavior.Strict);

            var controller = new RandomUserGeneratorDataController(logger.Object, logic.Object);

            var result = await controller.Get(numUsers);

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            logic.VerifyAll();
            logger.VerifyAll();
        }
    }
}
