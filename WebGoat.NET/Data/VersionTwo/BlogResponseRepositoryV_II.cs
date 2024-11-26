using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGoat.NET.Data.Interfaces;
using WebGoatCore.Data;
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
            //TODO: should put this in a try/catch
            _context.BlogResponses.Add(response);
            _context.SaveChanges();
        }
    }
}