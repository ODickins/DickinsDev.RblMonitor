using DickinsDev.RblMonitor.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DickinsDev.RblMonitor.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Data.DataContext _context;


        public HomeController(ILogger<HomeController> logger, Data.DataContext context, Utilities.Smtp.ISmtpClient smtpClient)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/Home/Check/{id}")]
        public IActionResult Check(string id)
        {
            System.Net.IPAddress IPAddress;
            if (!System.Net.IPAddress.TryParse(id, out IPAddress))
            {
                Models.Home.IndexModel indexModel = new Models.Home.IndexModel()
                {
                    IPAddress = id
                };
                ModelState.AddModelError("IPAddress", "Invalid IP Address");
                return View("Index", indexModel);
            }
            Models.Home.QuickCheckModel quickCheckModel = new Models.Home.QuickCheckModel();
            quickCheckModel.IPAddress = id;
            quickCheckModel.DnsCheckResult = Data.Functions.Static.CheckIP(id, _context.Nameservers.ToArray(), _context.DNSBLs.ToArray());

            return View("QuickCheck", quickCheckModel);
        }
        public IActionResult QuickCheck(Models.Home.IndexModel inputModel)
        {
            if (!ModelState.IsValid)
                return View("Index", inputModel);

            Models.Home.QuickCheckModel quickCheckModel = new Models.Home.QuickCheckModel();
            quickCheckModel.IPAddress = inputModel.IPAddress;
            quickCheckModel.DnsCheckResult = Data.Functions.Static.CheckIP(inputModel.IPAddress, _context.Nameservers.ToArray(), _context.DNSBLs.ToArray());

            return View(quickCheckModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
