using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Html;
using ControlOfRealEstate.Models.ForumViewModels;
using ControlOfRealEstate.TagHelpers;
using System.Text;
using System.Security.Cryptography;
using System;

namespace ControlOfRealEstate.HtmlHelpers
{
    public static class CommentHtmlHelper
    {
        public static HtmlString HierarchycalComments(List<CommentViewModel> comments, int? parentCommentId = null, bool isAuthenticated = false)
        {
            var commentsHtml = HierarchyComment(comments, parentCommentId, isAuthenticated);

            return new HtmlString(commentsHtml);
        }

        private static string HierarchyComment(List<CommentViewModel> allComments, int? parentCommentId, bool isAuthenticated)
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
                            <img src=""{ToGravatarUrl(childComment.Email, 50)}"">
                        </a>
                        <div class=""content"">
                            <input type=""hidden"" class=""comment-id"" value=""{childComment.CommentId}"" />
                            <a class=""author"">{childComment.UserName.ToLower()}</a>
                            <div class=""metadata"">
                                <time class=""timeago date"" datetime=""{childComment.CreatedAt.ToUniversalTime().ToString("yyyy-MM-ddTHH\\:mm\\:ssZ")}"">{childComment.CreatedAt}</time>
                            </div>
                            <div class=""text"">
                                {childComment.CommentText}
                            </div>";
                if (isAuthenticated)
                {
                    commentHtml += @"
                            <div class=""actions"">
                                <a class=""reply"">ответить</a>
                            </div>";
                }

                commentHtml += HierarchyComment(allComments, childComment.CommentId, isAuthenticated);
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

        public static string ToGravatarUrl(string email, int? size)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(email.Trim()))
                throw new ArgumentException("The email is empty.", nameof(email));

            var emailHash = ToGravatarHash(email);

            var imageUrl = "https://www.gravatar.com/avatar/" + emailHash;
            if (size.HasValue)
                imageUrl += "?s=" + size.Value;

            imageUrl += "&d=mm";

            return imageUrl;
        }

        private static string ToGravatarHash(string email)
        {
            var encoder = new UTF8Encoding();
            var md5 = MD5.Create();
            var hashedBytes = md5.ComputeHash(encoder.GetBytes(email.ToLower()));
            var sb = new StringBuilder(hashedBytes.Length * 2);

            for (var i = 0; i < hashedBytes.Length; i++)
                sb.Append(hashedBytes[i].ToString("X2"));

            return sb.ToString().ToLower();
        }
    }
}