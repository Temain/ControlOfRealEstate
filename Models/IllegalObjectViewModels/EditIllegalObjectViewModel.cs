using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControlOfRealEstate.Models.IllegalObjectViewModels
{
    public class EditIllegalObjectViewModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int IllegalObjectId { get; set; }

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
        public int StatusId { get; set; }

        /// <summary>
        /// Пользователь, подавший информацию об объекте
        /// </summary>
        public string ApplicationUserId { get; set; }
        public string ApplicationUserEmail { get; set; }
        public string ApplicationUserName { get; set; }

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
        /// Идентификатор из сервиса neagent.info
        /// </summary>
        public int? NeagentId { get; set; }

        /// <summary>
        /// Утверждено ли модератором
        /// </summary>
        public DateTime? ApprovedAt { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Статусы
        /// </summary>
        public SelectList Statuses { get; set; }
    }
}
