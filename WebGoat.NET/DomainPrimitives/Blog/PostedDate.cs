using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebGoat.NET.DomainPrimitives.Blog
{
    public class PostedDate
    {
        private DateTime _value;

        public PostedDate(DateTime value)
        {
            Validate(value);
            _value = value;
        }

        public DateTime GetValue(){
            return _value;
        }

        private void Validate(DateTime value){
            if(value == null){
                throw new ArgumentNullException("PostedDate must be filled");
            }
        }
    }
}