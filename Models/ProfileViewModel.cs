using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlOfRealEstate.Models.ForumViewModels;

namespace ControlOfRealEstate.Models
{
    public class ProfileViewModel
    {
        public string ApplicationUserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public List<IllegalObjectViewModel> IllegalObjects { get; set; }
        public List<ForumThreadViewModel> ForumThreads { get; set; }
        public List<CommentViewModel> Comments { get; set; }
    }
}
