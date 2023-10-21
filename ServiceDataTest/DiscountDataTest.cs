using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using ServiceData.ModelLayer;

namespace ServiceDataTest
{
    public class DiscountDataTest
    {

        private readonly ITestOutputHelper _extraOutput;
        readonly private IDiscount _discount;

        //readonly string _connectionString = "Server=Magnus-PC\\SQLEXPRESS; Integrated Security=true; Database=ServiceDB";
        readonly string _connectionString = "Server=localhost; Integrated Security=true; Database=x";

        public DiscountDataTest(ITestOutputHelper output)
        {
            _extraOutput = output;
            _discount = new DiscountDatabaseAccess(_connectionString);
        }
        [Fact]
        public void TestCreateDiscount()
        {
            //Arrange
            Discount disc = new Discount(12, 1, 2); // Creates object

            //Act
            int insertedId = _discount.CreateDiscount(disc); //Inserts object into database and returns ID

            //Assert
            Assert.True(insertedId > 0); //Asserts true if an Id was returned

            //Cleanup
            _discount.DeleteDiscountById(insertedId);//Deletes as cleanup
        }
        [Fact]
        public void TestDeleteDiscountById()
        {
            // Arrange
            Discount disc = new Discount(12, 1, 2); // Creates object
            int insertedId = _discount.CreateDiscount(disc); //Inserts object into database and returns ID

            // Act
            bool isDeleted = _discount.DeleteDiscountById(insertedId); //Deletes object

            // Assert
            Assert.True(isDeleted); //Asserts true if object is deleted.

        }
        [Fact]
        public void TestGetAllDiscounts()
        {
            // Arrange
            Discount disc = new Discount(12, 1, 2); // Creates object
            int insertedId = _discount.CreateDiscount(disc); //Inserts object into database and returns ID

            // Act
            List<Discount> readDiscounts = _discount.GetAllDiscount();
            bool IngredientsWereRead = (readDiscounts.Count > 0);
            // Print additional output
            _extraOutput.WriteLine("Number of Ingredients: " + readDiscounts.Count);

            // Assert
            Assert.True(IngredientsWereRead);

            //Cleanup
            bool isDeleted = _discount.DeleteDiscountById(insertedId); //Deletes object
            Assert.True(isDeleted); //Asserts true if object is deleted.

        }
        [Fact]
        public void TestUpdateDiscount()
        {
            // Arrange
            Discount disc = new Discount(12, 1, 2); // Creates object
            int insertedId = _discount.CreateDiscount(disc); //Inserts object into database and returns ID

            // Modify the discount object

            Discount updateDiscount = new Discount(insertedId, 13, 1, 2);

            // Act
            bool isUpdated = _discount.UpdateDiscountById(updateDiscount);

            // Retrieve the updated discount from the database
            Discount retrivedDiscount = _discount.GetDiscountById(insertedId);

            // Assert
            Assert.True(isUpdated); //Assert true if update went through
            Assert.NotNull(retrivedDiscount); //Asserts true of the retrieved object is not null
            Assert.Equal(insertedId, retrivedDiscount.Id); //Asserts true if insertedID and retrivedId is the same

            //Cleanup
            bool isDeleted = _discount.DeleteDiscountById(insertedId); //Deletes object
            Assert.True(isDeleted); //Asserts true if object is deleted.

        }

    }
}
