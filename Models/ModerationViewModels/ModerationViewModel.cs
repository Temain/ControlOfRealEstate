using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlOfRealEstate.Models.ModerationViewModels
{
    public class ModerationViewModel
    {
        public List<IllegalObjectViewModel> IllegalObjects { get; set; }
        public int PageSize { get; set; }
        public int PagesCount { get; set; }
        public int CurrentPage { get; set; }

        public int NotApprovedToday { get; set; }
        public int NotApprovedTotal { get; set; }
    }
}
