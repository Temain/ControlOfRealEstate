using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ControlOfRealEstate.Models
{
    public class IllegalObject
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
        /// Статус
        /// </summary>
        [ForeignKey("Status")]
        public int StatusId { get; set; }
        public IllegalObjectStatus Status { get; set; }

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
    }
}
