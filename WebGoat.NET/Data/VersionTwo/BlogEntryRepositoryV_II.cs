using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGoat.NET.Data.Interfaces;
using WebGoat.NET.DomainPrimitives.Blog;
using WebGoatCore.Data;
using WebGoatCore.DomainModels;
using WebGoatCore.Models;

namespace WebGoat.NET.Data.VersionTwo
{
    public class BlogEntryRepositoryV_II : IBlogEntryRepository
    {
        private readonly NorthwindContext _context;

        public BlogEntryRepositoryV_II(NorthwindContext context)
        {
            _context = context;
        }

        public BlogEntry CreateBlogEntry(string title, string contents, string username)
        {
            BlogEntryDM model = new BlogEntryDM(
                new Title(title),
                new PostedDate(DateTime.Now),
                new Contents(contents),
                new Author(username)
            );

            var entry = new BlogEntry
            {
                Title = model.Title.GetValue(),
                Contents = model.Title.GetValue(),
                Author = model.Author.GetValue(),
                PostedDate = model.PostedDate.GetValue(),
            };

            entry = _context.BlogEntries.Add(entry).Entity;
            _context.SaveChanges();
            return entry;
        }

        public BlogEntry GetBlogEntry(int blogEntryId)
        {
            BlogEntry blogEntry = _context.BlogEntries.Single(b => b.Id == blogEntryId);

            validateBlog(blogEntry);

            return blogEntry;
        }

        public List<BlogEntry> GetTopBlogEntries()
        {
            List<BlogEntry> blogs = GetTopBlogEntries(4, 0);

            List<BlogEntry> validatedBlogs = new List<BlogEntry>();

            foreach (var blog in blogs)
            {
                try
                {
                    validateBlog(blog);
                    validatedBlogs.Add(blog); 
                }
                catch (ArgumentException e)
                {
                    //Log the error here;
                }
            }

            return validatedBlogs;
        }

        public List<BlogEntry> GetTopBlogEntries(int numberOfEntries, int startPosition)
        {
            var blogEntries = _context.BlogEntries
                .OrderByDescending(b => b.PostedDate)
                .Skip(startPosition)
                .Take(numberOfEntries);
            return blogEntries.ToList();
        }

        private void validateBlog(BlogEntry blogEntry){
            new BlogEntryDM(
                new Title(blogEntry.Title),
                new PostedDate(blogEntry.PostedDate),
                new Contents(blogEntry.Contents),
                new Author(blogEntry.Author)
            ); 

            IList<BlogResponse> validatedResponses = new List<BlogResponse>();
            foreach (BlogResponse response in blogEntry.Responses)
            {
                BlogResponseDM blogDM = new BlogResponseDM(
                    new ResponseDate(response.ResponseDate),
                    new Contents(response.Contents),
                    new Author(response.Author),
                    new EntryId(response.BlogEntryId)
                );
                BlogResponse validatedResponse = new BlogResponse();
                validatedResponses.Add(validatedResponse);
            }
            blogEntry.Responses = 
        }
    }
}