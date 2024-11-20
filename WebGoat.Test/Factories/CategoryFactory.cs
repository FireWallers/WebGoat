using WebGoatCore.Models;

namespace WebGoat.Test.Factories
{
    public static class CategoryFactory
    {
        public static Category Create(int id, string name)
        {
            return new Category
            {
                CategoryId = id,
                CategoryName = name
            };
        }
    }
}
