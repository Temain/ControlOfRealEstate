using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlOfRealEstate.DataAccess;
using ControlOfRealEstate.Models;
using ControlOfRealEstate.Models.ForumViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControlOfRealEstate.Controllers
{
    public class ForumController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ForumController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(int? id)
        {
            ICollection<ForumThread> threads;
            var viewModel = new ForumThreadGroupViewModel();

            if (id != null)
            {
                var illegalObject = _context.IllegalObjects
                    .FirstOrDefault(x => x.IllegalObjectId == id);
                if (illegalObject == null)
                {
                    return NotFound();
                }

                viewModel.IllegalObjectId = id;
                viewModel.Address = illegalObject.Address;
                viewModel.Description = illegalObject.Description;
                viewModel.Infringement = illegalObject.Infringement;
                viewModel.ResultsOfReview = illegalObject.ResultsOfReview;

                threads = _context.ForumThreads
                    .Include(x => x.Comments)
                    .Where(x => x.IllegalObjectId == id)
                    .AsNoTracking()
                    .ToList();
            }
            else
            {
                threads = _context.ForumThreads
                    .Include(x => x.Comments)
                    .AsNoTracking()
                    .ToList();
            }

            viewModel.ForumThreads = threads
                .Select(x => new ForumThreadViewModel
                {
                    ForumThreadId = x.ForumThreadId,
                    Theme = x.Theme,
                    Description = x.Description,
                    LastUpdate = x.Comments
                        .DefaultIfEmpty(new Comment { CreatedAt = x.CreatedAt })
                        .Max(c => c.CreatedAt)
                })
                .OrderByDescending(x => x.LastUpdate)
                .ToList();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Thread(int id)
        {
            var thread = _context.ForumThreads
                .Include(x => x.Comments)
                .ThenInclude(x => x.ApplicationUser)
                .FirstOrDefault(x => x.ForumThreadId == id);
            if (thread == null)
            {
                return NotFound();
            }

            var viewModel = new ForumThreadViewModel
            {
                ForumThreadId = thread.ForumThreadId,
                IllegalObjectId = thread.IllegalObjectId,
                Description = thread.Description,
                Theme = thread.Theme,
                Comments = thread.Comments
                    .Select(x => new CommentViewModel
                    {
                        CommentId = x.CommentId,
                        UserName = x.ApplicationUser.UserName,
                        Email = x.ApplicationUser.Email,
                        CommentText = x.CommentText,
                        ForumThreadId = x.ForumThreadId,
                        ParentCommentId = x.ParentCommentId,
                        CreatedAt = x.CreatedAt
                    })
                    .OrderByDescending(x => x.CreatedAt)
                    .ToList()
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateThread(ForumThreadViewModel viewModel)
        {
            var thread = new ForumThread
            {
                IllegalObjectId = viewModel.IllegalObjectId,
                Theme = viewModel.Theme,
                Description = viewModel.Description,
                CreatedAt = DateTime.Now
            };

            _context.ForumThreads.Add(thread);
            _context.SaveChanges();

            viewModel.ForumThreadId = thread.ForumThreadId;
            viewModel.LastUpdate = thread.CreatedAt;

            return PartialView("_ThreadLite", viewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateComment(CommentViewModel viewModel)
        {
            var comment = new Comment
            {
                ForumThreadId = viewModel.ForumThreadId,
                CommentText = viewModel.CommentText,
                ParentCommentId = viewModel.ParentCommentId,
                CreatedAt = DateTime.Now
            };

            _context.Comments.Add(comment);
            _context.SaveChanges();

            viewModel.CommentId = comment.CommentId;
            viewModel.CreatedAt = comment.CreatedAt;

            return PartialView("_Comment", viewModel);
        }
    }
}