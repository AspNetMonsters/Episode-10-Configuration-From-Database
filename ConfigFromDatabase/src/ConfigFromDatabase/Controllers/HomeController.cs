using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Configuration;

namespace ConfigFromDatabase.Controllers
{
    public class HomeController : Controller
    {
        private IConfigurationRoot _configuration;

        public HomeController(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            ViewBag.SiteTitle = _configuration["SiteTitle"];
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
