using System;
using WebGoat.NET.DomainPrimitives.Blog;

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
namespace WebGoatCore.DomainModels
{
    public class BlogResponseDM
    {
        public int Id { get; set; }
        public EntryId EntryId { get; }
        public ResponseDate ResponseDate { get; }
        public Author Author { get; }
        public Contents Contents { get; }

        public virtual BlogEntryDM BlogEntry { get; }

        public BlogResponseDM(
            ResponseDate postedDate, 
            Contents contents, 
            Author author, 
            EntryId entryId)
        {
            ResponseDate = postedDate;
            Contents = contents;
            Author = author;
            EntryId = entryId;
        }
    }
}
