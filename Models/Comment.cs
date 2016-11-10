using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ControlOfRealEstate.Models
{
    /// <summary>
    /// Комментарий в ветке форума
    /// </summary>
    [Table("Comments")]
    public class Comment
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
        /// Ветка форума
        /// </summary>
        public int ForumThreadId { get; set; }
        public ForumThread ForumThread { get; set; }

        /// <summary>
        /// Коментарий может относиться к другому комментарию
        /// </summary>
        public int? ParentCommentId { get; set; }
        [ForeignKey("ParentCommentId")]
        public Comment ParentComment { get; set; }

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

        public ICollection<Comment> Comments { get; set; }
    }
}
