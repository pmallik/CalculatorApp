using CalculatorAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorAPI.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class HistoryController : ControllerBase
    {
        private static readonly Dictionary<string, string> HistoryData = [];
        
        [HttpGet]
        public ActionResult<Dictionary<string, string>> GetAll()
        {
            // This part of application is checking if user is authenticated or not 
            // to display history of data

            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }
            
            return Ok(HistoryData);
        }

        [HttpPost]
        public IActionResult Add([FromBody] HistoryItem item)
        {
            if (item == null || string.IsNullOrEmpty(item.Key) || string.IsNullOrEmpty(item.Value))
            {
                return BadRequest("Invalid input.");
            }

            HistoryData[item.Key] = item.Value;
            return Ok();
        }
    }
}

