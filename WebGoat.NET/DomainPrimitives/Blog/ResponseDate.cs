using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebGoat.NET.DomainPrimitives.Blog
{
    public class ResponseDate
    {
        private DateTime _value;

        public ResponseDate(DateTime value)
        {
            Validate(value);
            _value = value;
        }

        public DateTime GetValue(){
            return _value;
        }

        private void Validate(DateTime value){
            if(value == null){
                throw new ArgumentNullException("ResponseDate must be filled");
            }
        }
    }
}