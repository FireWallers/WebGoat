using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Web;
using WebGoat.NET.Helpers;

namespace WebGoat.NET.DomainPrimitives.Blog
{
    public class Contents
    {
        private string _value;

        public Contents(string value)
        {
            Validate(value);
            _value = HtmlSanitizer.Sanitize(value);
        }

        public string GetValue(){
            return _value;
        }

        private void Validate(string value){
            if(value == null){
                throw new ArgumentNullException("Content must be filled");
            }

            if(value.Length > 2400){
                throw new ArgumentException("Content is too long");
            }

            if(value.Length < 1){
                throw new ArgumentException("Content is too Short");
            }

            // string pattern = @"<(?:script|svg|iframe|object|embed|link|style|base|meta|form|img|audio|video|textarea)\b[^>]*>(.*?)</\1>|<(?:script|svg|iframe|object|embed|link|style|base|meta|form|img|audio|video|textarea)\b[^>]*\/?>";
            // Regex regex = new Regex(pattern);

            // if(!regex.IsMatch(value)){
            //     throw new ArgumentException("Content does not allow potentially malicious tags");
            // }
        }
    }
}