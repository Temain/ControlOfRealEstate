using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlOfRealEstate.Models.IllegalObjectViewModels
{
    public class IllegalObjectStatusViewModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int IllegalObjectStatusId { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string IllegalObjectStatusName { get; set; }

        /// <summary>
        /// Наименование файла для метки на карте
        /// </summary>
        public string IllegalObjectPlacemark { get; set; }
    }
}
