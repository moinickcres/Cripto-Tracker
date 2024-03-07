using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CriptoTracker.Models;

namespace CriptoTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackerController : ControllerBase
    {

        [HttpGet("{cryptocurrency}")]
        public async Task<ActionResult<decimal>> GetChange(string cryptocurrency)
        {
            decimal solution = await Tracker.GetPriceChange(cryptocurrency);

            return Ok(solution);
        }
    }
}
