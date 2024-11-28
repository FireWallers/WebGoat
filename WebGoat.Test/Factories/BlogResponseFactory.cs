using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGoatCore.Models;

namespace WebGoat.Test.Factories
{
    public static class BlogResponseFactory
    {
        public static BlogResponse Create(string author, string contents, int entryId, DateTime responseDate)
        {
            return new BlogResponse()
            {
                Author = author,
                Contents = contents,
                BlogEntryId = entryId,
                ResponseDate = responseDate
            };
        }
    }
}