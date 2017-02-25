using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ControlOfRealEstate.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ICollection<IllegalObject> IllegalObjects { get; set; }
        public ICollection<ForumThread> ForumThreads { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
