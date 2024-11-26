using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGoatCore.Models;

namespace WebGoat.NET.Data.Interfaces
{
    public interface IBlogResponseRepository
    {
        void CreateBlogResponse(BlogResponse response);
    }
}