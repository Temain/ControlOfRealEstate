using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlOfRealEstate.Models
{
    public class IllegalObjectViewModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int? IllegalObjectId { get; set; }

        /// <summary>
        /// Наименование нелегального объекта
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Идентификатор статуса
        /// </summary>
        public int? StatusId { get; set; }

        /// <summary>
        /// Наименование статуса
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// Наименование файла для метки на карте
        /// </summary>
        public string StatusPlacemark { get; set; }

        /// <summary>
        /// Цвет статуса
        /// </summary>
        public string StatusColor { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Выявленные нарушения
        /// </summary>
        public string Infringement { get; set; }

        /// <summary>
        /// Результаты рассмотрения
        /// </summary>
        public string ResultsOfReview { get; set; }

        /// <summary>
        /// Долгота
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Широта
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Идентификатор из сервиса neagent.info
        /// </summary>
        public int? NeagentId { get; set; } 

        /// <summary>
        /// Дата добавления
        /// </summary>
        public DateTime? CreatedAt { get; set; }
    }
}
