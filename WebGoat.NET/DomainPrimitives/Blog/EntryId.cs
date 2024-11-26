using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebGoat.NET.DomainPrimitives.Blog
{
    public class EntryId
    {
        private int _value;

        public EntryId(int value)
        {
            Validate(value);
            _value = value;
        }

        public int GetValue(){
            return _value;
        }

        private void Validate(int value){
            if(value == null){
                throw new ArgumentNullException("EntryId must be filled");
            }

            if(value < 1){
                throw new ArgumentException("EntryId is too Short");
            }
        }
    }
}