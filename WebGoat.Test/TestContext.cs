using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using WebGoatCore.Data;

namespace WebGoat.Test
{
    // public static class TestContext
    // {
    //     private static DbContextOptions<NorthwindContext> CreateInMemoryOptions()
    //     {
    //         return new DbContextOptionsBuilder<NorthwindContext>()
    //             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Ensures a unique database for each test
    //             .Options;
    //     }

    //     public static NorthwindContext CreateContext(){
    //         var options = CreateInMemoryOptions();
    //         return new NorthwindContext(options);
    //     }
    // }

    public static class TestContext
    {
        public static NorthwindContext CreateContext()
        {
            // Generate a unique connection string
            var connectionString = $"DataSource=file:{Guid.NewGuid()};Mode=Memory;Cache=Shared";

            var options = new DbContextOptionsBuilder<NorthwindContext>()
                .UseSqlite(connectionString)
                .Options;

            var context = new NorthwindContext(options);

            context.Database.OpenConnection(); // Open connection for shared cache
            context.Database.EnsureCreated();  // Create schema for this unique database

            return context;
        }
    }

}