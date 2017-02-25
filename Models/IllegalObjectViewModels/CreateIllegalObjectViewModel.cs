using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlOfRealEstate.Models.IllegalObjectViewModels
{
    public class CreateIllegalObjectViewModel
    {
        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Долгота
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Широта
        /// </summary>
        public double Latitude { get; set; }
    }
}
