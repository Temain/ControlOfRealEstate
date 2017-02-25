using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ControlOfRealEstate.Models
{
    /// <summary>
    /// Ветка форума
    /// </summary>
    [Table("ForumThreads")]
    public class ForumThread
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int ForumThreadId { get; set; }

        /// <summary>
        /// Тема обсуждения
        /// </summary>
        public string Theme { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Объект обсуждения
        /// </summary>
        public int IllegalObjectId { get; set; }
        public IllegalObject IllegalObject { get; set; }

        /// <summary>
        /// Пользователь, добавивший ветку
        /// </summary>
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

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
        /// Комментарии
        /// </summary>
        public ICollection<Comment> Comments { get; set; }
    }
}
