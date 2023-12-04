using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using ServiceData.ModelLayer;

namespace ServiceDataTest
{
    public class DiscountDataTest
    { /*
        private readonly ITestOutputHelper _extraOutput;
        readonly private IDiscount _discount;

        readonly string _connectionString = "Server=localhost; Integrated Security=true; Database=x";

        public DiscountDataTest(ITestOutputHelper output)
        {
            _extraOutput = output;
            _discount = new DiscountDatabaseAccess(_connectionString);
        }

        [Fact]
        public async Task TestCreateDiscount()
        {
            // Arrange
            Discount disc = new Discount(12, 1, 2); // Creates object

            // Act
            int insertedId = await _discount.CreateDiscount(disc); // Inserts object into the database and returns ID

            // Assert
            Assert.True(insertedId > 0); // Asserts true if an ID was returned

            // Cleanup
            bool isDeleted = await _discount.DeleteDiscountById(insertedId); // Deletes as cleanup
            Assert.True(isDeleted); // Asserts true if the object is deleted.
        }

        [Fact]
        public async Task TestDeleteDiscountById()
        {
            // Arrange
            Discount disc = new Discount(12, 1, 2); // Creates object
            int insertedId = await _discount.CreateDiscount(disc); // Inserts object into the database and returns ID

            // Act
            bool isDeleted = await _discount.DeleteDiscountById(insertedId); // Deletes object

            // Assert
            Assert.True(isDeleted); // Asserts true if the object is deleted.
        }

        [Fact]
        public async Task TestGetAllDiscounts()
        {
            // Arrange
            Discount disc = new Discount(12, 1, 2); // Creates object
            int insertedId = await _discount.CreateDiscount(disc); // Inserts object into the database and returns ID

            // Act
            List<Discount> readDiscounts = await _discount.GetAllDiscount();
            bool DiscountsWereRead = (readDiscounts.Count > 0);

            // Print additional output
            _extraOutput.WriteLine("Number of Discounts: " + readDiscounts.Count);

            // Assert
            Assert.True(DiscountsWereRead);

            // Cleanup
            bool isDeleted = await _discount.DeleteDiscountById(insertedId); // Deletes object
            Assert.True(isDeleted); // Asserts true if the object is deleted.
        }

        [Fact]
        public async Task TestUpdateDiscount()
        {
            // Arrange
            Discount disc = new Discount(12, 1, 2); // Creates object
            int insertedId = await _discount.CreateDiscount(disc); // Inserts object into the database and returns ID

            // Modify the discount object
            Discount updateDiscount = new Discount(insertedId, 13, 1, 2);

            // Act
            bool isUpdated = await _discount.UpdateDiscountById(updateDiscount);

            // Retrieve the updated discount from the database
            Discount retrievedDiscount = await _discount.GetDiscountById(insertedId);

            // Assert
            Assert.True(isUpdated); // Assert true if the update went through
            Assert.NotNull(retrievedDiscount); // Asserts true if the retrieved object is not null
            Assert.Equal(insertedId, retrievedDiscount.Id); // Asserts true if insertedID and retrievedId are the same

            // Cleanup
            bool isDeleted = await _discount.DeleteDiscountById(insertedId); // Deletes the object
            Assert.True(isDeleted); // Asserts true if the object is deleted.
        } */
    }
}
