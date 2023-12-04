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
{ /*
    public class ShopDataTest
    {
        private readonly ITestOutputHelper _extraOutput;
        private readonly IShop _shopAccess;

        private readonly string _connectionString = "Server=Magnus-PC\\SQLEXPRESS; Integrated Security = true; Database=ServiceDB";

        public ShopDataTest(ITestOutputHelper output)
        {
            _extraOutput = output;
            _shopAccess = new ShopDatabaseAccess(_connectionString);
        }

        [Fact]
        public async Task TestCreateShop()
        {
            // Arrange
            Shop shop = new Shop("JensensBøfhus", "Bedehuset", Shop.Storetype.Restaurant);

            // Act
            int insertedId = await _shopAccess.CreateShop(shop);

            // Assert
            Assert.True(insertedId > 0);

            // Cleanup
            await _shopAccess.DeleteShopById(insertedId);
        }

        [Fact]
        public async Task TestDeleteShopById()
        {
            // Arrange
            Shop shop = new Shop("JensensBøfhus", "Bedehuset", Shop.Storetype.Restaurant);
            int insertedId = await _shopAccess.CreateShop(shop);

            // Act
            bool isDeleted = await _shopAccess.DeleteShopById(insertedId);

            // Assert
            Assert.True(isDeleted);
        }

        [Fact]
        public async Task TestGetAllShops()
        {
            // Arrange
            Shop shop = new Shop("JensensBøfhus", "Bedehuset", Shop.Storetype.Restaurant);
            int insertedId = await _shopAccess.CreateShop(shop);

            // Act
            List<Shop> readShops = await _shopAccess.GetAllShops();
            bool shopsWereRead = (readShops.Count > 0);

            // Assert
            Assert.True(shopsWereRead);

            // Cleanup
            await _shopAccess.DeleteShopById(insertedId);
        }

        [Fact]
        public async Task TestGetShopById()
        {
            // Arrange
            Shop shop = new Shop("JensensBøfhus", "Bedehuset", Shop.Storetype.Restaurant);
            int insertedId = await _shopAccess.CreateShop(shop);

            // Act
            Shop retrievedShop = await _shopAccess.GetShopById(insertedId);

            // Assert
            Assert.NotNull(retrievedShop);
            Assert.Equal(shop.Name, retrievedShop.Name);

            // Cleanup
            await _shopAccess.DeleteShopById(insertedId);
        }

        [Fact]
        public async Task TestUpdateShopById()
        {
            // Arrange
            Shop shop = new Shop("JensensBøfhus", "Bedehuset", Shop.Storetype.Restaurant);
            int insertedId = await _shopAccess.CreateShop(shop);

            // Modify the shop
            Shop updatedShop = new Shop(insertedId, "UpdatedShopName", "UpdatedShopLocation", Shop.Storetype.Restaurant);

            // Act
            bool isUpdated = await _shopAccess.UpdateShopById(updatedShop);

            // Retrieve the updated shop from the database
            Shop retrievedShop = await _shopAccess.GetShopById(insertedId);

            // Assert
            Assert.True(isUpdated);
            Assert.NotNull(retrievedShop);
            Assert.Equal(updatedShop.Name, retrievedShop.Name);
            Assert.Equal(updatedShop.Type, retrievedShop.Type);

            // Cleanup
            await _shopAccess.DeleteShopById(insertedId);
        } 
    } */
}
