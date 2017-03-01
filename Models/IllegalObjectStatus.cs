using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ControlOfRealEstate.Models
{
    /// <summary>
    /// Статус проведения проверки
    /// </summary>
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

        /// <summary>
        /// Наименование файла для метки на карте
        /// </summary>
        public string IllegalObjectPlacemark { get; set; }

        /// <summary>
        /// Цвет статуса
        /// </summary>
        public string IllegalObjectColor { get; set; }

        /// <summary>
        /// Порядок сортировки
        /// </summary>
        public int? IllegalObjectStatusOrder { get; set; }

        public ICollection<IllegalObject> IllegalObjects { get; set; }
    }
}
