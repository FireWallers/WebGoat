using System;
using WebGoat.NET.DomainPrimitives.Blog;

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
namespace WebGoatCore.DomainModels
{
    public class BlogResponseDM
    {
        public int Id { get; set; }
        public EntryId EntryId { get; set; }
        public ResponseDate ResponseDate { get; set; }
        public Author Author { get; set; }
        public Contents Contents { get; set; }

        public virtual BlogEntryDM BlogEntry { get; set; }

        public BlogResponseDM(ResponseDate postedDate, Contents contents, Author author, EntryId entryId)
        {
            ResponseDate = postedDate;
            Contents = contents;
            Author = author;
            EntryId = entryId;
        }
    }
}
