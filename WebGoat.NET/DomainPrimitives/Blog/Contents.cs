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
            _value = Validate(value);
        }

        public string GetValue(){
            return _value;
        }

        private string Validate(string value){
            if(value.Length > 2400){
                throw new ArgumentException("Content is too long");
            }

            if(value.Length < 1){
                throw new ArgumentException("Content is too Short");
            }

            return HtmlValidater.Validate(value);
        }
    }
}