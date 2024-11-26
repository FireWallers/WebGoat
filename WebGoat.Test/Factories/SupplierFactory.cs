using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGoatCore.Models;

namespace WebGoat.Test.Factories
{
    public class SupplierFactory
    {
        public static Supplier Create(
            int id, 
            string companyName,
            string contactName
            )
        {
            return new Supplier
            {
                SupplierId = id,
                CompanyName = companyName,
                ContactName = contactName
            };
        }
    }
}