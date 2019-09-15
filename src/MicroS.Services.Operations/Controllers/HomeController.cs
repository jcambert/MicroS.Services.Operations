using Microsoft.AspNetCore.Mvc;

namespace MicroS.Services.Operations.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("MicroSShop Operations Service");
    }
}
