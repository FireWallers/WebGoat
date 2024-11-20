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
            var service = new BlogResponseRepository(context); // Assuming BlogService contains the CreateBlogEntry method

            string author = "Test Author";
            string contents = "This is a test content.";
            int entryId = 21;
            DateTime now = DateTime.Now;
            BlogResponse response = BlogResponseFactory.Create(author, contents, entryId, now);
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

        [Fact]
        public void Test_that_can_not_insert_code_into_blog()
        {
            // Arrange
            NorthwindContext context = TestContext.CreateContext();
            var service = new BlogEntryRepository(context);

            string maliciousContent = "<script>alert('XSS')</script>";
            // string sanitizedContent = "alert('XSS')"; // Adjust based on your sanitization logic
            string author = "Test Author";
            string title = "A Title";
            DateTime now = DateTime.Now;


            // Act
            var result = service.CreateBlogEntry(title, maliciousContent, author);

            // Assert
            var entryInDb = context.BlogEntries
                .FirstOrDefault(e => e.Id == result.Id);
            Assert.NotNull(entryInDb);
            Assert.NotEqual(maliciousContent, entryInDb.Contents); // Ensure raw malicious content is not saved
            // Assert.Equal(sanitizedContent, entryInDb.Contents);   // Ensure content is sanitized correctly
        }

        [Fact]
        public void Test_that_can_not_insert_code_into_reply()
        {
            // Arrange
            NorthwindContext context = TestContext.CreateContext();
            var service = new BlogResponseRepository(context);

            string maliciousContent = "<script>alert('XSS')</script>";
            // string sanitizedContent = "alert('XSS')"; // Adjust based on your sanitization logic
            string author = "Test Author";
            int entryId = 21;
            DateTime now = DateTime.Now;

            BlogResponse response = BlogResponseFactory.Create(author, maliciousContent, entryId, now);

            // Act
            service.CreateBlogResponse(response);

            // Assert
            var entryInDb = context.BlogResponses
                .FirstOrDefault(e => e.BlogEntryId == entryId);
            Assert.NotNull(entryInDb);
            Assert.NotEqual(maliciousContent, entryInDb.Contents); // Ensure raw malicious content is not saved
            // Assert.Equal(sanitizedContent, entryInDb.Contents);   // Ensure content is sanitized correctly
        }
        //End Security Test
    }
}
