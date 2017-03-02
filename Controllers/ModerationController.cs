using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlOfRealEstate.DataAccess;
using ControlOfRealEstate.Models;
using ControlOfRealEstate.Models.ModerationViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControlOfRealEstate.Controllers
{
    public class ModerationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ModerationController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index(string query = null, int page = 1, int pageSize = 25, bool today = false, bool total = false)
        {
            var illegalObjects = _context.IllegalObjects
                .Include(x => x.Status)
                .Where(x => x.DeletedAt == null)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new IllegalObjectViewModel
                {
                    IllegalObjectId = x.IllegalObjectId,
                    Address = x.Address,
                    Description = x.Description,
                    Infringement = x.Infringement,
                    ResultsOfReview = x.ResultsOfReview,
                    StatusId = x.StatusId,
                    StatusName = x.Status.IllegalObjectStatusName,
                    StatusColor = x.Status.IllegalObjectColor,
                    CreatedAt = x.CreatedAt,
                    ApprovedAt = x.ApprovedAt                    
                });

            var totalObjects = illegalObjects.Count();
            var notApprovedTotal = illegalObjects.Count(x => x.ApprovedAt == null);
            var notApprovedToday = illegalObjects.Count(x => x.ApprovedAt == null && (x.CreatedAt != null && x.ApprovedAt != null && x.CreatedAt.Value.Date == DateTime.Now.Date));

            if (query != null)
            {
                illegalObjects = illegalObjects.Where(x => x.Address.Contains(query));
            }

            if (total)
            {
                illegalObjects = illegalObjects.Where(x => x.ApprovedAt == null);
            }

            if (today)
            {
                illegalObjects = illegalObjects.Where(x => x.ApprovedAt == null && (x.CreatedAt != null && x.ApprovedAt != null && x.CreatedAt.Value.Date == DateTime.Now.Date));
            }

            var illegalObjectsList = illegalObjects
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToList();       

            var viewModel = new ModerationViewModel
            {
                IllegalObjects = illegalObjectsList,
                PagesCount = (int)Math.Ceiling((double) illegalObjects.Count() / pageSize),
                PageSize = pageSize,
                CurrentPage = page,
                Query = query,
                Total = totalObjects,
                NotApprovedTotal = notApprovedTotal,
                NotApprovedToday = notApprovedToday,
                IsNotApprovedTodayList = today,
                IsNotApprovedTotalList = total
            };

            return View(viewModel);
        }

        public IActionResult Approve(List<int> illegalObjectIds, bool cancel = false)
        {
            var illegalObjects = _context.IllegalObjects.Where(x => illegalObjectIds.Contains(x.IllegalObjectId) && x.DeletedAt == null);
            if (cancel)
            {
                illegalObjects = illegalObjects.Where(x => x.ApprovedAt != null);
            }
            else
            {
                illegalObjects = illegalObjects.Where(x => x.ApprovedAt == null);
            }

            foreach(var illegalObject in illegalObjects)
            {
                if (cancel)
                {
                    illegalObject.ApprovedAt = null;
                }
                else
                {
                    illegalObject.ApprovedAt = DateTime.Now;
                }
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}