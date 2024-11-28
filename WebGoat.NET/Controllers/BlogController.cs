using WebGoatCore.Models;
using WebGoatCore.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using WebGoat.NET.Data.Interfaces;
using WebGoatCore.DomainModels;
using WebGoat.NET.DomainPrimitives.Blog;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebGoatCore.ViewModels;

namespace WebGoatCore.Controllers
{
    [Route("[controller]/[action]")]
    public class BlogController : Controller
    {
        private readonly IBlogEntryRepository _blogEntryRepository;
        private readonly IBlogResponseRepository _blogResponseRepository;

        public BlogController(IBlogEntryRepository blogEntryRepository, IBlogResponseRepository blogResponseRepository, NorthwindContext context)
        {
            _blogEntryRepository = blogEntryRepository;
            _blogResponseRepository = blogResponseRepository;
        }

        public IActionResult Index()
        {
            return View(_blogEntryRepository.GetTopBlogEntries());
        }

        [HttpGet("{entryId}")]
        public IActionResult Reply(int entryId)
        {
            return View(_blogEntryRepository.GetBlogEntry(entryId));
        }

        [HttpPost("{entryId}")]
        public IActionResult Reply(int entryId, string contents)
        {
            try
            {
                var userName = User?.Identity?.Name ?? "Anonymous";

                var response = new BlogResponse()
                {
                    Author = userName,
                    Contents = contents,
                    BlogEntryId = entryId,
                    ResponseDate = DateTime.Now
                };
                _blogResponseRepository.CreateBlogResponse(response);

                return RedirectToAction("Index");   
            }
            catch (ArgumentException argument)
            {
                //TODO Somehow return the message from the exception
                return View(_blogEntryRepository.GetBlogEntry(entryId));
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create() => View();

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(string title, string contents)
        {
            try
            {
                var blogEntry = _blogEntryRepository.CreateBlogEntry(title, contents, User!.Identity!.Name!);
                return View(blogEntry);    
            }
            catch (ArgumentException argument)
            {
                return View();
            }
        }

    }
}