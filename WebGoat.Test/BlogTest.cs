using WebGoatCore.Data;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebGoatCore.Models;
using WebGoat.Test.Factories;
using WebGoat.NET.Data.VersionTwo;
using WebGoat.NET.Data.Interfaces;

namespace WebGoat.Test
{
    [Trait("Category", "BlogTests")]
    public class BlogTest
    {
        [Fact]
        public void Test_that_can_create_blog()
        {
            // Arrange
            NorthwindContext context = TestContext.CreateContext();
            var service = new BlogEntryRepository(context); // Assuming BlogService contains the CreateBlogEntry method

            string title = "Test Title";
            string contents = "This is a test content.";
            string username = "testuser";

            // Act
            var result = service.CreateBlogEntry(title, contents, username);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(title, result.Title);
            Assert.Equal(contents, result.Contents);
            Assert.Equal(username, result.Author);
            Assert.NotEqual(default, result.PostedDate);

            // Verify it exists in the database
            var entryInDb = context.BlogEntries.FirstOrDefault(e => e.Id == result.Id);
            Assert.NotNull(entryInDb);
            Assert.Equal(result.Id, entryInDb.Id);
        }

        [Fact]
        public void Test_that_can_reply_to_blog()
        {
            // Arrange
            NorthwindContext context = TestContext.CreateContext();
            var blogEntryRepo = new BlogEntryRepository(context);
            string username = "testuser";
            string author = "Test Author";
            string contents = "This is a test content.";

            BlogEntry result = blogEntryRepo.CreateBlogEntry(author, contents, username);

            int entryId = result.Id;
            DateTime now = DateTime.Now;
            BlogResponse response = BlogResponseFactory.Create(author, contents, entryId, now);
            var service = new BlogResponseRepository(context);

            // Act
            service.CreateBlogResponse(response);

            // Assert
            // Verify it exists in the database
            var entryInDb = context.BlogResponses
                .FirstOrDefault(e => e.BlogEntryId == entryId);
            Assert.NotNull(entryInDb);
            Assert.Equal(contents, entryInDb.Contents);
            Assert.Equal(author, entryInDb.Author);
            Assert.Equal(entryId, entryInDb.BlogEntryId);
        }

        //SECURITY TEST START FROM HERE!!!

        [Theory]
        // [InlineData(typeof(BlogEntryRepository))]
        [InlineData(typeof(BlogEntryRepositoryV_II))]
        public void Test_that_can_not_insert_code_into_blog(Type repositoryType)
        {
            // Arrange
            NorthwindContext context = TestContext.CreateContext();
            var service = (IBlogEntryRepository)Activator.CreateInstance(repositoryType, context);

            string maliciousContent = "<script>alert('XSS')</script>";
            string author = "Test Author";
            string title = "A Title";
            DateTime now = DateTime.Now;

            BlogEntry createdEntry = null;

            // Act
            try
            {
                service.CreateBlogEntry(title, maliciousContent, author);
            }
            catch (ArgumentException exception)
            {
                // Check if an entry was created despite the exception
                createdEntry = context.BlogEntries.FirstOrDefault(e => e.Title == title && e.Author == author);
            }

            // Assert
            if (createdEntry != null)
            {
                Assert.NotEqual(maliciousContent, createdEntry.Contents); // Ensure raw malicious content is not saved
            }
            else
            {
                Assert.Null(context.BlogEntries.FirstOrDefault(e => e.Title == title && e.Author == author));
            }
        }


        [Theory]
        // [InlineData(typeof(BlogResponseRepository))]
        [InlineData(typeof(BlogResponseRepositoryV_II))]
        public void Test_that_can_not_insert_code_into_reply(Type repositoryType)
        {
            // Arrange
            NorthwindContext context = TestContext.CreateContext();
            var service = (IBlogResponseRepository)Activator.CreateInstance(repositoryType, context);

            string maliciousContent = "<script>alert('XSS')</script>";
            // string sanitizedContent = "alert('XSS')"; // Adjust based on your sanitization logic
            string author = "Test Author";
            DateTime now = DateTime.Now;
            BlogEntry entry = BlogEntryFactory.Create(author, "Content", "Test User");
            var createdEntry = context.BlogEntries.Add(entry).Entity;
            context.SaveChangesAsync();
            BlogResponse response = BlogResponseFactory.Create(author, maliciousContent, createdEntry.Id, now);

            BlogResponse createdResponse = null;

            // Act
            try
            {
                service.CreateBlogResponse(response);
            }
            catch (ArgumentException exception)
            {
                // Check if an entry was created despite the exception
                createdResponse = context.BlogResponses.FirstOrDefault(e => e.Contents == maliciousContent && e.Author == author);
            }

            // Assert
            if (createdResponse != null)
            {
                Assert.NotEqual(maliciousContent, createdResponse.Contents); // Ensure raw malicious content is not saved
            }
            else
            {
                Assert.Null(context.BlogResponses.FirstOrDefault(e => e.Contents == maliciousContent && e.Author == author));
            }
        }
        //End Security Test
    }
}
