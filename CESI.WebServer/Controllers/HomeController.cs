using CESI.WebServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace CESI.WebServer.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly WebServerConfiguration _configuration;

		public HomeController(ILogger<HomeController> logger, IOptions<WebServerConfiguration> configuration)
		{
			_logger = logger;
			_configuration = configuration.Value;
		}

		public IActionResult Index()
		{			
			return View(_configuration);
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