using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using ServiceData.DatabaseLayer;
using ServiceData.ModelLayer;
using ServiceData.DatabaseLayer.Interfaces;

namespace ServiceDataTest
{
    public class ShopDataTest
    {
        private readonly ITestOutputHelper _extraOutput;
        private readonly IShop _shopAccess;

        //readonly string _connectionString = "Server=localhost; Integrated Security=true; Database=x";
        private readonly string _connectionString = "Server=Magnus-PC\\SQLEXPRESS; Integrated Security = true; Database=ServiceDB";

        public ShopDataTest(ITestOutputHelper output)
        {
            _extraOutput = output;
            _shopAccess = new ShopDatabaseAccess(_connectionString);
        }

        [Fact]
        public void TestCreateShop()
        {
            // Arrange
            Shop shop = new Shop("JensensBøfhus", "Bedehuset", Shop._Type.Restuarant);

            // Act
            int insertedId = _shopAccess.CreateShop(shop);

            // Assert
            Assert.True(insertedId > 0);

            // Cleanup
            _shopAccess.DeleteShopById(insertedId);
        }

        [Fact]
        public void TestDeleteShopById()
        {
            // Arrange
            Shop shop = new Shop("ShopName", "ShopLocation", Shop._Type.Restuarant);
            int insertedId = _shopAccess.CreateShop(shop);

            // Act
            bool isDeleted = _shopAccess.DeleteShopById(insertedId);

            // Assert
            Assert.True(isDeleted);
        }

        [Fact]
        public void TestGetAllShops()
        {
            // Arrange
            Shop shop = new Shop("ShopName", "ShopLocation", Shop._Type.Restuarant);
            int insertedId = _shopAccess.CreateShop(shop);

            // Act
            List<Shop> readShops = _shopAccess.GetAllShops();
            bool shopsWereRead = (readShops.Count > 0);

            // Assert
            Assert.True(shopsWereRead);

            // Cleanup
            _shopAccess.DeleteShopById(insertedId);
        }

        [Fact]
        public void TestGetShopById()
        {
            // Arrange
            Shop shop = new Shop("ShopName", "ShopLocation", Shop._Type.Restuarant);
            int insertedId = _shopAccess.CreateShop(shop);

            // Act
            Shop retrievedShop = _shopAccess.GetShopById(insertedId);

            // Assert
            Assert.NotNull(retrievedShop);
            Assert.Equal(shop.Name, retrievedShop.Name);

            // Cleanup
            _shopAccess.DeleteShopById(insertedId);
        }

        [Fact]
        public void TestUpdateShopById()
        {
            // Arrange
            Shop shop = new Shop("ShopName", "ShopLocation", Shop._Type.Restuarant);
            int insertedId = _shopAccess.CreateShop(shop);

            // Modify the shop
            Shop updatedShop = new Shop(insertedId, "UpdatedShopName", "UpdatedShopLocation", Shop._Type.FoodStand);

            // Act
            bool isUpdated = _shopAccess.UpdateShopById(updatedShop);

            // Retrieve the updated shop from the database
            Shop retrievedShop = _shopAccess.GetShopById(insertedId);

            // Assert
            Assert.True(isUpdated);
            Assert.NotNull(retrievedShop);
            Assert.Equal(updatedShop.Name, retrievedShop.Name);
            Assert.Equal(updatedShop.Type, retrievedShop.Type);

            // Cleanup
            _shopAccess.DeleteShopById(insertedId);
        }
    }
}
