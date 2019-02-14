using System;
using Microsoft.AspNetCore.Mvc;

namespace PartControllerLib
{
    public class MyPartController : Controller
    {
        public IActionResult Index()
        {
            return Ok("This is an Application Part Controller's Action method");
        }
    }
}
