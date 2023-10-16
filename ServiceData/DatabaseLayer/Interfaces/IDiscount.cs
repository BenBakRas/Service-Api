using ServiceData.ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.DatabaseLayer.Interfaces
{
    public interface IDiscount
    {
        Discount GetDiscountById(int id);
        List<Discount> GetAllDiscount();
        int CreateDiscount(Discount anDiscount);
        bool DeleteDiscountById(int id);
        bool UpdateDiscountById(Discount DiscountToUpdate);
    }
}
