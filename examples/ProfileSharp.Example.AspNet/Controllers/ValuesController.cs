using Microsoft.AspNetCore.Mvc;
using ProfileSharp.Attributes;
using System.Threading.Tasks;

namespace ProfileSharp.Example.AspNet.Controllers
{
    [EnableProfileSharp]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("ProcessInt/Sync/{value}")]
        public IActionResult ProcessInt(int value)
        {
            return Ok(value);
        }

        [HttpGet("ProcessInt/Async/{value}")]
        public async Task<IActionResult> ProcessIntAsync(int value)
        {
            return await Task.Factory.StartNew(() => Ok(value));
        }

        [HttpPost("ProcessObject/Sync")]
        public IActionResult ProcessInt([FromBody] PersonDto value)
        {
            return Ok(value);
        }

        [HttpPost("ProcessObject/Async")]
        public async Task<IActionResult> ProcessIntAsync([FromBody] PersonDto value)
        {
            return await Task.Factory.StartNew(() => Ok(value));
        }
    }

    public class PersonDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }
    }
}
