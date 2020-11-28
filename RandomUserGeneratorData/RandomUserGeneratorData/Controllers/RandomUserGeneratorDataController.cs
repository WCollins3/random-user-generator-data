using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RandomUserGeneratorData.Core.Logic;

namespace RandomUserGeneratorData.Controllers
{
    /// <summary>
    ///     Controller used to retrieve data related to collections of randomly-generated users.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RandomUserGeneratorDataController : ControllerBase
    {
        private readonly ILogger<RandomUserGeneratorDataController> _logger;

        private IRandomUserGeneratorLogic _randomUserGeneratorLogic;

        public RandomUserGeneratorDataController(ILogger<RandomUserGeneratorDataController> logger, IRandomUserGeneratorLogic randomUserGeneratorLogic)
        {
            _logger = logger;
            _randomUserGeneratorLogic = randomUserGeneratorLogic;
        }

        /// <summary>
        ///     GET method for data related to randomly-generated users.
        /// </summary>
        /// <param name="numUsers">Number of users to retrieve data on.</param>
        /// <returns>A <see cref="RandomUserGeneratorData"/> object related to the user data retrieved.</returns>
        [HttpGet("numberOfUsers/{numUsers:int}/data")]
        public async Task<IActionResult> Get(int numUsers)
        {
            if (numUsers < 1)
            {
                return new BadRequestObjectResult($"Must request at least one user. Number of users requested: {numUsers}.");
            }

            try
            {
                var randomUserGeneratorDataHolder = await _randomUserGeneratorLogic.GetRandomUserGeneratorDataHolder(numUsers);
                return Ok(randomUserGeneratorDataHolder);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
