using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ControlOfRealEstate.Controllers
{
    public class ForumController : Controller
    {
        public IActionResult Thread(int id)
        {
            return View();
        }
    }
}