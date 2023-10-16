using ServiceData.ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.DatabaseLayer.Interfaces
{
    public interface IShop
    {
        Shop GetShopById(int id);
        List<Shop> GetAllShops();
        int CreateShop(Shop anShop);
        bool DeleteShopById(int id);
        bool UpdateShopById(Shop ShopToUpdate);

    }
}
