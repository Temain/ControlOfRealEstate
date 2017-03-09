using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlOfRealEstate.DataAccess;
using ControlOfRealEstate.Models;
using ControlOfRealEstate.Models.ForumViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControlOfRealEstate.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string userId)
        {
            ApplicationUser user = null;
            if (userId != null)
            {
                user = await _userManager.FindByIdAsync(userId);
            }
            else
            {
                user = await _userManager.GetUserAsync(User);
            }    

            var illegalObjects = _context.IllegalObjects
                .Include(x => x.Status)
                .Where(x => x.ApplicationUserId == user.Id && x.DeletedAt == null)
                .Select(x => new IllegalObjectViewModel
                {
                    IllegalObjectId = x.IllegalObjectId,
                    Address = x.Address,
                    Description = x.Description,
                    Infringement = x.Infringement,
                    ResultsOfReview = x.ResultsOfReview,
                    StatusName = x.Status.IllegalObjectStatusName,
                    StatusColor = x.Status.IllegalObjectColor,
                    CreatedAt = x.CreatedAt
                })
                .OrderByDescending(x => x.CreatedAt)
                .ToList();

            var forumThreads = _context.ForumThreads
                .Include(x => x.IllegalObject)
                .Where(x => x.ApplicationUserId == user.Id && x.DeletedAt == null)
                .Select(x => new ForumThreadViewModel
                {
                    ForumThreadId = x.ForumThreadId,
                    Theme = x.Theme,
                    LastUpdate = x.CreatedAt,
                    Description = x.IllegalObject.Address
                })
                .OrderByDescending(x => x.LastUpdate)
                .ToList();

            var comments = _context.Comments
                .Where(x => x.ApplicationUserId == user.Id && x.DeletedAt == null)
                .Select(x => new CommentViewModel
                {
                    CommentText = x.CommentText,
                    ForumThreadId = x.ForumThreadId,
                    CreatedAt = x.CreatedAt
                })
                .OrderByDescending(x => x.CreatedAt)
                .ToList();

            var viewModel = new ProfileViewModel
            {
                ApplicationUserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                IllegalObjects = illegalObjects,
                ForumThreads = forumThreads,
                Comments = comments
            };

            return View(viewModel);
        }
    }
}