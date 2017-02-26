using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlOfRealEstate.Models.ForumViewModels
{
    public class CommentViewModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int CommentId { get; set; }

        /// <summary>
        /// Текст комметария
        /// </summary>
        public string CommentText { get; set; }

        /// <summary>
        /// Автор комментария
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Электронный адрес автора
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Ветка форума
        /// </summary>
        public int ForumThreadId { get; set; }

        /// <summary>
        /// Коментарий может относиться к другому комментарию
        /// </summary>
        public int? ParentCommentId { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
