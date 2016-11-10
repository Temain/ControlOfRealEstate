using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlOfRealEstate.DataAccess;
using ControlOfRealEstate.Models;
using ControlOfRealEstate.Models.ForumViewModels;
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

        public IActionResult Index(int? id)
        {
            IQueryable<ForumThread> threads;
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
                    .AsNoTracking();
            }
            else
            {
                threads = _context.ForumThreads
                    .Include(x => x.Comments)
                    .AsNoTracking();
            }

            viewModel.ForumThreads = threads
                .Select(x => new ForumThreadViewModel
                {
                    ForumThreadId = x.ForumThreadId,
                    Theme = x.Theme,
                    LastUpdate = x.Comments
                        .OrderByDescending(c => c.CreatedAt)
                        .FirstOrDefault()
                        .CreatedAt
                })
                // .OrderByDescending(x => x.LastUpdate)
                .ToList();

            return View(viewModel);
        }

        public IActionResult Thread(int id)
        {
            var thread = _context.ForumThreads
                .Include(x => x.Comments)
                .FirstOrDefault(x => x.ForumThreadId == id);
            if (thread == null)
            {
                return NotFound();
            }

            var viewModel = new ForumThreadViewModel
            {
                ForumThreadId = thread.ForumThreadId,
                Theme = thread.Theme,
                Comments = thread.Comments
                    .Select(x => new CommentViewModel
                    {
                        CommentId = x.CommentId,
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
    }
}