using Microsoft.AspNetCore.Mvc;

namespace Rover.App.Controllers
{
    public class ErrorController : ControllerBase
    {
        public IActionResult Error() => Problem();
    }
}
