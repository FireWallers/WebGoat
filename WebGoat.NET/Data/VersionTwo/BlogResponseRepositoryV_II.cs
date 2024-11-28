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
    public class BlogResponseRepositoryV_II : IBlogResponseRepository
    {
        private readonly NorthwindContext _context;

        public BlogResponseRepositoryV_II(NorthwindContext context)
        {
            _context = context;
        }

        public void CreateBlogResponse(BlogResponse response)
        {
            new BlogResponseDM(
                new ResponseDate(response.ResponseDate),
                new Contents(response.Contents),
                new Author(response.Author),
                new EntryId(response.BlogEntryId)
            );
            //TODO: should put this in a try/catch
            _context.BlogResponses.Add(response);
            _context.SaveChanges();
        }
    }
}