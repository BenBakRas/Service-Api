using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using ServiceData.DatabaseLayer;
using ServiceData.ModelLayer;
using ServiceData.DatabaseLayer.Interfaces;

namespace ServiceDataTest
{
    public class ProductDataTest
    {
        private readonly ITestOutputHelper _extraOutput;
        readonly private IProduct _productAccess;

        readonly string _connectionString = "Server=Magnus-PC\\SQLEXPRESS; Integrated Security = true; Database=ServiceDB";

        public ProductDataTest(ITestOutputHelper output)
        {
            _extraOutput = output;
            _productAccess = new ProductDatabaseAccess(_connectionString);
        }

        [Fact]
        public async Task TestCreateProduct()
        {
            // Arrange
            Product prod1 = new Product("12", "Hamburger", 50, 212141, Product._Category.Burgere, 1);

            // Act
            int insertedId = await _productAccess.CreateProduct(prod1);

            // Assert
            Assert.True(insertedId > 0);

            // Cleanup
            await _productAccess.DeleteProductById(insertedId);
        }

        [Fact]
        public async Task TestDeleteProductById()
        {
            // Arrange
            Product prod1 = new Product("1", "Hamburger", 50, 212141, Product._Category.Burgere, 1);
            int insertedId = await _productAccess.CreateProduct(prod1);

            // Act
            bool isDeleted = await _productAccess.DeleteProductById(insertedId);

            // Assert
            Assert.True(isDeleted);

            // Cleanup
            await _productAccess.DeleteProductById(insertedId);
        }

        [Fact]
        public async Task TestGetAllProducts()
        {
            // Arrange
            Product prod1 = new Product("1", "Hamburger", 50, 212141, Product._Category.Burgere, 1);
            int insertedId = await _productAccess.CreateProduct(prod1);

            // Act
            List<Product> readProducts = await _productAccess.GetAllProducts();
            bool productsWereRead = (readProducts.Count > 0);
            _extraOutput.WriteLine("Number of Products: " + readProducts.Count);

            // Assert
            Assert.True(productsWereRead);

            // Cleanup
            await _productAccess.DeleteProductById(insertedId);
        }

        [Fact]
        public async Task TestUpdateProduct()
        {
            // Arrange
            Product prod1 = new Product("1", "Hamburger", 50, 212141, Product._Category.Burgere, 1);
            int insertedId = await _productAccess.CreateProduct(prod1);

            // Modify the Lane object
            Product updatedProd = new Product(insertedId, "2", "Pomfritter", 60, 212112, Product._Category.Sides, 1);

            // Act
            bool isUpdated = await _productAccess.UpdateProductById(updatedProd);

            // Retrieve the updated prod from the database
            Product retrievedProd = await _productAccess.GetProductById(insertedId);

            // Assert
            Assert.True(isUpdated);
            Assert.NotNull(retrievedProd);
            Assert.Equal(insertedId, retrievedProd.Id);
            Assert.Equal(retrievedProd.BasePrice, 60);

            // Cleanup
            await _productAccess.DeleteProductById(insertedId);
        }
    }
}
