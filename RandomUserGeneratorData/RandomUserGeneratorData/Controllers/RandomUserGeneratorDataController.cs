using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RandomUserGeneratorData.Core.Logic;
using RandomUserGeneratorData.Core.Models;

namespace RandomUserGeneratorData.Controllers
{
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

        [HttpGet("numberOfUsers/{numUsers:int}/data")]
        public async Task<IActionResult> Get(int numUsers)
        {
            if (numUsers < 1)
            {
                return new BadRequestObjectResult($"Must request at least one user. Number of users requested: {numUsers}.");
            }

            var randomUserGeneratorDataHolder = await _randomUserGeneratorLogic.GetRandomUserGeneratorDataHolder(numUsers);
            return Ok(randomUserGeneratorDataHolder);
        }
    }
}
