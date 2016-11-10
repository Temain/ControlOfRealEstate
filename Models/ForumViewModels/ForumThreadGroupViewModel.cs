using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlOfRealEstate.Models.ForumViewModels
{
    public class ForumThreadGroupViewModel
    {
        /// <summary>
        /// Идентификатор объекта
        /// </summary>
        public int? IllegalObjectId { get; set; }

        /// <summary>
        /// Описание объекта
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Адрес объекта
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Выявленные нарушения
        /// </summary>
        public string Infringement { get; set; }

        /// <summary>
        /// Результаты рассмотрения
        /// </summary>
        public string ResultsOfReview { get; set; }

        /// <summary>
        /// Ветки форума
        /// </summary>
        public List<ForumThreadViewModel> ForumThreads { get; set; }
    }
}
