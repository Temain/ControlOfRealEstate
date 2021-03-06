using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ControlOfRealEstate.TagHelpers
{
    [HtmlTargetElement("span", Attributes = EmailAttributeName)]
    public class GravatarImageTagHelper : TagHelper
    {
        private const string SizeAttributeName = "image-size";
        private const string EmailAttributeName = "gravatar-email";
        private const string AltTextAttributeName = "alt";

        const string GravatarBaseUrl = "https://www.gravatar.com/avatar/";

        [HtmlAttributeName(EmailAttributeName)]
        public string Email { get; set; }

        [HtmlAttributeName(AltTextAttributeName)]
        public string AltText { set; get; }

        [HtmlAttributeName(SizeAttributeName)]
        public int? Size { get; set; }

        private string ToGravatarHash(string email)
        {
            var encoder = new UTF8Encoding();
            var md5 = MD5.Create();
            var hashedBytes = md5.ComputeHash(encoder.GetBytes(email.ToLower()));
            var sb = new StringBuilder(hashedBytes.Length * 2);

            for (var i = 0; i < hashedBytes.Length; i++)
                sb.Append(hashedBytes[i].ToString("X2"));

            return sb.ToString().ToLower();
        }

        private string ToGravatarUrl(string email, int? size)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(email.Trim()))
                throw new ArgumentException("The email is empty.", nameof(email));

            var emailHash = ToGravatarHash(email);

            var imageUrl = GravatarBaseUrl + emailHash;
            if (size.HasValue)
                imageUrl += "?s=" + size.Value;

            imageUrl += "&d=mm";

            return imageUrl;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var str = new StringBuilder();
            var url = ToGravatarUrl(this.Email, this.Size);
            str.AppendFormat("<img src='{0}' alt='{1}' />", url, AltText);
            output.Content.AppendHtml(str.ToString());
        }
    }
}
