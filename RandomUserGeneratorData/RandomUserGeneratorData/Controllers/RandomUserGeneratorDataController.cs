using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RandomUserGeneratorData.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RandomUserGeneratorDataController : ControllerBase
    {
        private readonly ILogger<RandomUserGeneratorDataController> _logger;

        public RandomUserGeneratorDataController(ILogger<RandomUserGeneratorDataController> logger)
        {
            _logger = logger;
        }

        [HttpGet("numberOfUsers/{numUsers:int}/data")]
        public int Get(int numUsers)
        {
            return numUsers;
        }
    }
}
