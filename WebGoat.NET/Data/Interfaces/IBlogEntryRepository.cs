using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGoatCore.Models;

namespace WebGoat.NET.Data.Interfaces
{
    public interface IBlogEntryRepository
    {
        public BlogEntry CreateBlogEntry(string title, string contents, string username);

        public BlogEntry GetBlogEntry(int blogEntryId);

        public List<BlogEntry> GetTopBlogEntries();

        public List<BlogEntry> GetTopBlogEntries(int numberOfEntries, int startPosition);
    }
}