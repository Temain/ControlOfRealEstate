using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ControlOfRealEstate.Models
{
    public class IllegalObjectStatus
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int IllegalObjectStatusId { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string IllegalObjectStatusName { get; set; }

        public ICollection<IllegalObject> IllegalObjects { get; set; }
    }
}
