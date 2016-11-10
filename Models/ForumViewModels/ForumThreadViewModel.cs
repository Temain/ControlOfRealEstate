using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlOfRealEstate.Models.ForumViewModels
{
    public class ForumThreadViewModel
    {
        public int ForumThreadId { get; set; }

        public string Theme { get; set; }

        public DateTime LastUpdate { get; set; }

        public List<CommentViewModel> Comments { get; set; }
    }
}
