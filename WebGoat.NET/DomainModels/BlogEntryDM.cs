using System;
using System.Collections.Generic;
using WebGoat.NET.DomainPrimitives.Blog;

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
namespace WebGoatCore.DomainModels
{
    public class BlogEntryDM
    {
        public int Id { get; set; }
        public Title Title { get; set; }
        public PostedDate PostedDate { get; set; }
        public Contents Contents { get; set; }
        public Author Author { get; set; }

        public virtual IList<BlogResponseDM> Responses { get; set; }

        public BlogEntryDM(Title title, PostedDate postedDate, Contents contents, Author author)
        {
            Title = title;
            PostedDate = postedDate;
            Contents = contents;
            Author = author;
        }
    }
}