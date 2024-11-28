using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGoatCore.Models;

namespace WebGoat.Test.Factories
{
    public class BlogEntryFactory
    {
        public static BlogEntry Create(string title, string contents, string username)
        {
            return new BlogEntry
                {
                    Title = title,
                    Contents = contents,
                    Author = username,
                    PostedDate = DateTime.Now,
                };
        }
    }
}