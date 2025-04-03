using System.Diagnostics;
using Internship.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Internship.Controllers
{
    public class HomeController : Controller
    {
        public IConfiguration Configuration { get; }
       

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
        }

        public IActionResult Index()
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];//line 23
            SqlConnection connection = new SqlConnection(connectionString);//line 24
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
