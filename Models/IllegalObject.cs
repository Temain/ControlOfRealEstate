using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ControlOfRealEstate.Models
{
    /// <summary>
    /// Нелегально построенный объект (или легально, в зависимости от результатов проверки)
    /// </summary>
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
        /// Пользователь, подавший информацию об объекте
        /// </summary>
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

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
        /// Утверждено ли модератором
        /// </summary>
        public DateTime? ApprovedAt { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Дата обновления
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Дата удаления
        /// </summary>
        public DateTime? DeletedAt { get; set; }

        /// <summary>
        /// Обсуждения на форуме
        /// </summary>
        public ICollection<ForumThread> ForumThreads { get; set; }
    }
}
