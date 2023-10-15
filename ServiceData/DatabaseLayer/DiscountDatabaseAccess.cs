using Microsoft.Extensions.Configuration;
using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.DatabaseLayer
{
    public class DiscountDatabaseAccess : IDiscount
    {
        readonly string? _connectionString;

        public DiscountDatabaseAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CompanyConnection");
        }

        public DiscountDatabaseAccess(string inConnectionString)
        {
            _connectionString = inConnectionString;
        }

        public int CreateDiscount(Discount anDiscount)
        {
            throw new NotImplementedException();
        }

        public bool DeleteDiscountById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Discount> GetAllDiscount()
        {
            throw new NotImplementedException();
        }

        public Discount GetDiscountById(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateDiscountById(Discount DiscountToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
