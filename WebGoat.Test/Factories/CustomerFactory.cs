using WebGoatCore.Models;

namespace WebGoat.Test.Factories
{
    public static class CustomerFactory
    {
        public static Customer Create(string id, string companyName, string contactName)
        {
            return new Customer
            {
                CustomerId = id,
                CompanyName = companyName,
                ContactName = contactName
            };
        }
    }
}
