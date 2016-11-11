using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlOfRealEstate.Models.ForumViewModels
{
    public class ForumThreadViewModel
    {
        /// <summary>
        /// Идентификатор ветки
        /// </summary>
        public int ForumThreadId { get; set; }

        /// <summary>
        /// Идентификатор объекта обсуждения
        /// </summary>
        public int? IllegalObjectId { get; set; }

        /// <summary>
        /// Тема обсуждения
        /// </summary>
        public string Theme { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Последнее обновление
        /// </summary>
        public DateTime LastUpdate { get; set; }

        /// <summary>
        /// Комментарии
        /// </summary>
        public List<CommentViewModel> Comments { get; set; }
    }
}
