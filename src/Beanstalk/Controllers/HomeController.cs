
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Beanstalk.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Beanstalk.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration _config;
        private ILogger<HomeController> _logger;

        public HomeController(IConfiguration cfg, ILogger<HomeController> log)
        {
            _config = cfg;
            _logger = log;
        }

        public IActionResult Index()
        {
            var path = HttpContext.Request.Path;
            var ip = HttpContext.Connection.RemoteIpAddress.MapToIPv4();
            _logger.LogInformation($"{path} from {ip}");

            return View();
        }
        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
