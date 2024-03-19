using Microsoft.AspNetCore.Mvc;
using NetLink.Playground.Models;
using System.Diagnostics;
using NetLink.Models;
using NetLink.Session;


namespace NetLink.Playground.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEndUserSessionManager _endUserSessionManager;

        public HomeController(ILogger<HomeController> logger, IEndUserSessionManager endUserSessionManager)
        {
            _logger = logger;
            _endUserSessionManager = endUserSessionManager;
        }

        public IActionResult Index()
        {
            EndUser user1 = new EndUser("4567-e89b-12d3-a456-426614174000");
            _endUserSessionManager.LogEndUserIn(user1);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
