using Microsoft.AspNetCore.Mvc;

namespace Beanstalk.Controllers
{
    public class StatusController : Controller
    {
        // Health endpoint for EB
        public IActionResult Index() 
        {
            return Ok();
        }
    }
}