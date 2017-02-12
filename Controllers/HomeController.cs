using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlOfRealEstate.DataAccess;
using ControlOfRealEstate.Models.IllegalObjectViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControlOfRealEstate.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var iilegalObjectStatuses = _context.IllegalObjectStatuses
                .AsNoTracking()
                .OrderBy(x => x.IllegalObjectStatusOrder)
                .Select(x => new IllegalObjectStatusViewModel
                {
                    IllegalObjectStatusId = x.IllegalObjectStatusId,
                    IllegalObjectStatusName = x.IllegalObjectStatusName,
                    IllegalObjectPlacemark = x.IllegalObjectPlacemark
                })
                .ToList();
            ViewBag.IllegalObjectStatuses = iilegalObjectStatuses;

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
