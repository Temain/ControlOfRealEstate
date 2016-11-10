using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Html;
using ControlOfRealEstate.Models.ForumViewModels;

namespace ControlOfRealEstate.HtmlHelpers
{
    public static class CommentHelpers
    {
        public static HtmlString HierarchycalComments(List<CommentViewModel> comments, int? parentCommentId = null)
        {
            var commentsHtml = HierarchyComment(comments, parentCommentId);

            return new HtmlString(commentsHtml);
        }

        private static string HierarchyComment(List<CommentViewModel> allComments, int? parentCommentId)
        {
            var commentHtml = "";
            var childComments = allComments
                .Where(c => c.ParentCommentId == parentCommentId)
                .ToList();

            if (childComments.Any())
            {
                commentHtml += "<div class=\"comments ui\">";
            }

            foreach (CommentViewModel childComment in childComments)
            {
                commentHtml += 
                    $@"<div class=""comment"">
                        <a class=""avatar"">
                            <img src=""http://semantic-ui.com/images/avatar/small/matt.jpg"">
                        </a>
                        <div class=""content"">
                            <a class=""author"">Matt</a>
                            <div class=""metadata"">
                                <time class=""timeago date"" datetime=""{childComment.CreatedAt.ToUniversalTime().ToString("yyyy-MM-ddTHH\\:mm\\:ssZ")}"">{childComment.CreatedAt}</time>
                            </div>
                            <div class=""text"">
                                {childComment.CommentText}
                            </div>
                            <div class=""actions"">
                                <a class=""reply"">Комментировать</a>
                            </div>";

                commentHtml += HierarchyComment(allComments, childComment.CommentId);
                commentHtml += 
                        @"</div>
                    </div>";
            }

            if (childComments.Any())
            {
                commentHtml += "</div>";
            }

            return commentHtml;
        }
    }
}