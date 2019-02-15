using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace PartClassLib
{
    public class NetCoreLibDemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
