using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using ServiceData.DatabaseLayer;
using ServiceData.ModelLayer;
using ServiceData.DatabaseLayer.Interfaces;

namespace ServiceDataTest
{
    public class OrdersDataTest
    {
        private readonly ITestOutputHelper _extraOutput;
        readonly private IOrders _ordersAccess;

        readonly string _connectionString = "Server=Magnus-PC\\SQLEXPRESS; Integrated Security = true; Database=ServiceDB";

        public OrdersDataTest(ITestOutputHelper output)
        {
            _extraOutput = output;
            _ordersAccess = new OrdersDatabaseAccess(_connectionString);
        }
        [Fact]
        public void TestCreateOrder()
        {
            //Arrange
            Orders order1 = new Orders(1, DateTime.Now, 20.00, 1);

            //Act
            int insertedId = _ordersAccess.CreateOrder(order1); //Creates object and inserts into database and returns ID

            //Assert
            Assert.True(insertedId > 0); //Asserts true if an Id was returned

            //Cleanup
            _ordersAccess.DeleteOrderById(insertedId); //Deletes as cleanup
        }
        /*
        [Fact]
        public void TestDeleteProductById()
        {
            // Arrange
            Product prod1 = new Product("1", "Hamburger", 50, 212141, Product._Category.Burgere, 1); //creates object
            int insertedId = _productAccess.CreateProduct(prod1); // Inserts object to Database

            // Act
            bool isDeleted = _productAccess.DeleteProductById(insertedId);//Deletes object

            // Assert
            Assert.True(isDeleted); //Asserts true if object is deleted.

            //Cleanup
            _productAccess.DeleteProductById(insertedId); //Deletes as cleanup

        }
        [Fact]
        public void TestGetAllProducts()
        {
            // Arrange
            Product prod1 = new Product("1", "Hamburger", 50, 212141, Product._Category.Burgere, 1); //creates object
            int insertedId = _productAccess.CreateProduct(prod1); // Inserts object to Database

            // Act
            List<Product> readProducts = _productAccess.GetAllProducts();
            bool productsWereRead = (readProducts.Count > 0);
            // Print additional output
            _extraOutput.WriteLine("Number of Products: " + readProducts.Count);

            // Assert
            Assert.True(productsWereRead);

            // Cleanup
            _productAccess.DeleteProductById(insertedId);
        }
        
        
        [Fact]
        public void TestUpdateProduct()
        {
            // Arrange
            Product prod1 = new Product("1", "Hamburger", 50, 212141, Product._Category.Burgere, 1); //creates object
            int insertedId = _productAccess.CreateProduct(prod1); // Inserts object to Database

            // Modify the Lane object
            Product updatedProd = new Product(insertedId, "2", "Pomfritter", 60, 212112, Product._Category.Sides, 1);

            // Act
            bool isUpdated = _productAccess.UpdateProductById(updatedProd);

            // Retrieve the updated prod from the database
            Product retrievedProd = _productAccess.GetProductById(insertedId);

            // Assert
            Assert.True(isUpdated); //Assert true if update went through
            Assert.NotNull(retrievedProd); //Asserts true of the retrieved object is not null
            Assert.Equal(insertedId, retrievedProd.Id); //Asserts true if insertedID and retrivedId is the same
            Assert.Equal(retrievedProd.BasePrice, 60); //Asserts true if retrived parameter equals given parameter, 10 in this case. 

            //Cleanup
            _productAccess.DeleteProductById(insertedId);
        }
        */
    }
}