using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlOfRealEstate.DataAccess;
using ControlOfRealEstate.Models;
using ControlOfRealEstate.Models.IllegalObjectViewModels;
using ControlOfRealEstate.Models.ModerationViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult ApproveObject(List<int> illegalObjectIds, bool cancel = false)
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

        [HttpGet]
        public IActionResult EditObject(int illegalObjectId)
        {
            var illegalObject = _context.IllegalObjects
                .Include(x => x.Status)
                .Include(x => x.ApplicationUser)
                .SingleOrDefault(x => x.IllegalObjectId == illegalObjectId && x.DeletedAt == null);

            var statuses = _context.IllegalObjectStatuses
                .Select(x => new
                {
                    Id = x.IllegalObjectStatusId,
                    Value = x.IllegalObjectStatusName
                });

            var viewModel = new EditIllegalObjectViewModel
            {
                IllegalObjectId = illegalObject.IllegalObjectId,
                Address = illegalObject.Address,
                Description = illegalObject.Description,
                StatusId = illegalObject.StatusId,
                Infringement = illegalObject.Infringement,
                ResultsOfReview = illegalObject.ResultsOfReview,
                ApplicationUserId = illegalObject.ApplicationUserId,
                ApplicationUserEmail = illegalObject.ApplicationUser.Email,
                ApplicationUserName = illegalObject.ApplicationUser.UserName,
                CreatedAt = illegalObject.CreatedAt,
                ApprovedAt = illegalObject.ApprovedAt,
                Name = illegalObject.Name,
                NeagentId = illegalObject.NeagentId,
                Statuses = new SelectList(statuses, "Id" , "Value")
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult EditObject(EditIllegalObjectViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var illegalObject = _context.IllegalObjects
                .SingleOrDefault(x => x.IllegalObjectId == viewModel.IllegalObjectId && x.DeletedAt == null);
            if (illegalObject == null)
            {
                return NotFound();
            }

            illegalObject.Description = viewModel.Description;
            illegalObject.StatusId = viewModel.StatusId;
            illegalObject.ResultsOfReview = viewModel.ResultsOfReview;
            illegalObject.Infringement = viewModel.Infringement;

            _context.SaveChanges();

            return RedirectToAction("Index", "Moderation");
        }
    }
}