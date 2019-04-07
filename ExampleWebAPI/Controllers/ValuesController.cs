using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExampleWebAPI.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ExampleWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IRandomNumberGenerator _rand;

        public ValuesController(IRandomNumberGenerator rand)
        {
            _rand = rand ?? throw new ArgumentNullException(nameof(rand));
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<int>> Get()
        {
            return new int[]
            { 
                _rand.Generate(),
                _rand.Generate(),
                _rand.Generate(),
                _rand.Generate()
            };
        }
    }
}
