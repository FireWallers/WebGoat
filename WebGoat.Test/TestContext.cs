using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebGoatCore.Data;

namespace WebGoat.Test
{
    public static class TestContext
    {
        private static DbContextOptions<NorthwindContext> CreateInMemoryOptions()
        {
            return new DbContextOptionsBuilder<NorthwindContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Ensures a unique database for each test
                .Options;
        }

        public static NorthwindContext CreateContext(){
            var options = CreateInMemoryOptions();
            return new NorthwindContext(options);
        }
    }
}