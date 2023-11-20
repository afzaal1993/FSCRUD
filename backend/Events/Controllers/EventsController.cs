using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Events.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        [HttpGet]
        [Route("GetImage")]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        public async Task<IActionResult> GetImage(string fileName)
        {
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
            string filePath = Path.Combine(directoryPath, fileName);

            byte[] bytes = System.IO.File.ReadAllBytes(filePath);
            string file = Convert.ToBase64String(bytes);

            var result = ApiResponse<string>.Success(file);

            return Ok(result);
        }
    }
}
