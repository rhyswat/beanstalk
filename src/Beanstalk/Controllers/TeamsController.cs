

using System;
using System.Linq;
using Beanstalk.Db.Context;
using Beanstalk.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Beanstalk.Controllers
{
    public class TeamsController : Controller
    {
        private BeanstalkDbContext _context;
        private ILogger<TeamsController> _logger;

        public TeamsController(BeanstalkDbContext context, ILogger<TeamsController> log)
        {
            _context = context;
            _logger = log;
        }

        public IActionResult Index()
        {
            var path = HttpContext.Request.Path;
            var ip = HttpContext.Connection.RemoteIpAddress.MapToIPv4();
            _logger.LogInformation($"{path} from {ip}");

            var teams = _context.Teams.Select(t => new TeamViewModel(t)).ToList();
            var model = new TeamCollectionViewModel { Teams = teams, Timestamp = DateTime.Now };
            return View(model);
        }
    }
}
