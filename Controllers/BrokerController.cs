using HomeFinder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFinder.Controllers
{
    public class BrokerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
