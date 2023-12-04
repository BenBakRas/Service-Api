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
    { /*
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
        
        [Fact]
        public void TestDeleteProductById()
        {
            // Arrange
            OrderLine orderLine1 = new OrderLine(100, 1, 6);
            int insertedId = _orderLineAccess.CreateOrderLine(orderLine1); //Creates object and inserts into database and returns ID

            // Act
            bool isDeleted = _orderLineAccess.DeleteOrderLineById(insertedId);//Deletes object

            // Assert
            Assert.True(isDeleted); //Asserts true if object is deleted.

            //Cleanup
            _orderLineAccess.DeleteOrderLineById(insertedId); //Deletes as cleanup

        }
        
        [Fact]
        public void TestGetAllOrderLines()
        {
            // Arrange
            OrderLine orderLine1 = new OrderLine(100, 1, 6); //creates object
            int insertedId = _orderLineAccess.CreateOrderLine(orderLine1);  // Inserts object to Database

            // Act
            List<OrderLine> readOrderLine = _orderLineAccess.GetAllOrderLines();
            bool orderLinesWereRead = (readOrderLine.Count > 0);
            // Print additional output
            _extraOutput.WriteLine("Number of orders: " + readOrderLine.Count);

            // Assert
            Assert.True(orderLinesWereRead);

            // Cleanup
            _orderLineAccess.DeleteOrderLineById(insertedId);
        }
        
        
        [Fact]
        public void TestUpdateProduct()
        {
            // Arrange
            OrderLine orderLine1 = new OrderLine(100, 1, 6); //creates object
            int insertedId = _orderLineAccess.CreateOrderLine(orderLine1);  // Inserts object to Database

            // Modify the Lane object
            OrderLine updatedOrderLine = new OrderLine(insertedId, 120, 2, 1);

            // Act
            bool isUpdated = _orderLineAccess.UpdateOrderLineById(updatedOrderLine);

            // Retrieve the updated prod from the database
            OrderLine retrievedOrderLine = _orderLineAccess.GetOrderLineById(insertedId);

            // Assert
            Assert.True(isUpdated); //Assert true if update went through
            Assert.NotNull(retrievedOrderLine); //Asserts true of the retrieved object is not null
            Assert.Equal(insertedId, retrievedOrderLine.Id); //Asserts true if insertedID and retrivedId is the same
            Assert.Equal(120, retrievedOrderLine.OrderlinePrice); //Asserts true if retrived parameter equals given parameter, 40 in this case. 

            //Cleanup
            _orderLineAccess.DeleteOrderLineById(insertedId);
        }
        */
        
    }
}