using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace WebGoat.NET.Helpers
{
    public static class HtmlValidater
        {
            private static readonly string[] DenyListTags = { "script", "iframe", "object", "embed", "form" };
            private static readonly string[] DenyListAttributes = { "onload", "onclick", "onerror", "href", "src" };

            public static string Validate(string input)
            {
                if (string.IsNullOrEmpty(input))
                    return string.Empty;

                // Remove blacklisted tags
                foreach (var tag in DenyListTags)
                {
                    var tagRegex = new Regex($"<\\/?\\s*{tag}\\s*[^>]*>", RegexOptions.IgnoreCase);
                    if(tagRegex.IsMatch(input)){
                        throwInvalid();
                    }
                    // input = tagRegex.Replace(input, string.Empty);
                }

                // Remove blacklisted attributes
                foreach (var attr in DenyListAttributes)
                {
                    var attrRegex = new Regex($"{attr}\\s*=\\s*['\"].*?['\"]", RegexOptions.IgnoreCase);
                    if(attrRegex.IsMatch(input)){
                        throwInvalid();
                    }
                    // input = attrRegex.Replace(input, string.Empty);
                }

                // Remove javascript: links
                var jsLinkRegex = new Regex(@"href\s*=\s*['""]javascript:[^'""]*['""]", RegexOptions.IgnoreCase);
                if(jsLinkRegex.IsMatch(input)){
                    throwInvalid();
                }
                // input = jsLinkRegex.Replace(input, string.Empty);

                // Remove plain http and https links
                var plainLinkRegex = new Regex(@"(http|https):\/\/[^\s<>]+", RegexOptions.IgnoreCase);
                if(plainLinkRegex.IsMatch(input)){
                    throwInvalid();
                }
                // input = plainLinkRegex.Replace(input, string.Empty);

                // Encode any remaining HTML
                return HttpUtility.HtmlEncode(input);
            }

            private static void throwInvalid(){
                throw new ArgumentException("Input has malicious tags");
            }
        }
}