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
    public class OrderLineDataTest
    {
        private readonly ITestOutputHelper _extraOutput;
        readonly private IOrderLine _orderLineAccess;

        readonly string _connectionString = "Server=Magnus-PC\\SQLEXPRESS; Integrated Security = true; Database=x";
        //readonly string _connectionString = "Server=localhost; Integrated Security=true; Database=x";

        public OrderLineDataTest(ITestOutputHelper output)
        {
            _extraOutput = output;
            _orderLineAccess = new OrderLineDatabaseAccess(_connectionString);
        }
        
        [Fact]
        public void TestCreateOrderLine()
        {
            //Arrange
            OrderLine orderLine1 = new OrderLine(100, 1, 6);

            //Act
            int insertedId = _orderLineAccess.CreateOrderLine(orderLine1); //Creates object and inserts into database and returns ID

            //Assert
            Assert.True(insertedId > 0); //Asserts true if an Id was returned

            //Cleanup
            _orderLineAccess.DeleteOrderLineById(insertedId); //Deletes as cleanup
        }
        /*
        [Fact]
        public void TestDeleteProductById()
        {
            // Arrange
            Orders order1 = new Orders(1, DateTime.Now, 20.00, 1);
            int insertedId = _ordersAccess.CreateOrder(order1); //Creates object and inserts into database and returns ID

            // Act
            bool isDeleted = _ordersAccess.DeleteOrderById(insertedId);//Deletes object

            // Assert
            Assert.True(isDeleted); //Asserts true if object is deleted.

            //Cleanup
            _ordersAccess.DeleteOrderById(insertedId); //Deletes as cleanup

        }
        [Fact]
        public void TestGetAllProducts()
        {
            // Arrange
            Orders order1 = new Orders(1, DateTime.Now, 20.00, 1); //creates object
            int insertedId = _ordersAccess.CreateOrder(order1);  // Inserts object to Database

            // Act
            List<Orders> readOrders = _ordersAccess.GetAllOrders();
            bool productsWereRead = (readOrders.Count > 0);
            // Print additional output
            _extraOutput.WriteLine("Number of orders: " + readOrders.Count);

            // Assert
            Assert.True(productsWereRead);

            // Cleanup
            _ordersAccess.DeleteOrderById(insertedId);
        }
        
        
        [Fact]
        public void TestUpdateProduct()
        {
            // Arrange
            Orders order1 = new Orders(1, DateTime.Now, 20.00, 1); //creates object
            int insertedId = _ordersAccess.CreateOrder(order1); // Inserts object to Database

            // Modify the Lane object
            Orders updatedOrder = new Orders(insertedId, DateTime.Now, 40, 1);

            // Act
            bool isUpdated = _ordersAccess.UpdateOrderById(updatedOrder);

            // Retrieve the updated prod from the database
            Orders retrievedOrder = _ordersAccess.GetOrderById(insertedId);

            // Assert
            Assert.True(isUpdated); //Assert true if update went through
            Assert.NotNull(retrievedOrder); //Asserts true of the retrieved object is not null
            Assert.Equal(insertedId, retrievedOrder.Id); //Asserts true if insertedID and retrivedId is the same
            Assert.Equal(40, retrievedOrder.TotalPrice); //Asserts true if retrived parameter equals given parameter, 40 in this case. 

            //Cleanup
            _ordersAccess.DeleteOrderById(insertedId);
        }
        */
        
    }
}