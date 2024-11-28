using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebGoat.NET.DomainPrimitives.Blog
{
    public class Author
    {
        private string _value;

        public Author(string value)
        {
            Validate(value);
            _value = value;
        }

        public string GetValue(){
            return _value;
        }

        private void Validate(string value){
            if(value == null){
                throw new ArgumentNullException("Author must be filled");
            }

            if(value.Length > 50){
                throw new ArgumentException("Author is too long");
            }

            if(value.Length < 2){
                throw new ArgumentException("Author is too Short");
            }

            string pattern = @"^[a-zA-Z -']+$";
            Regex regex = new Regex(pattern);

            if(!regex.IsMatch(value)){
                throw new ArgumentException("Author contains non valid characters");
            }
        }
    }
}