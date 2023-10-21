using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using ServiceData.DatabaseLayer;
using ServiceData.ModelLayer;
using ServiceData.DatabaseLayer.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace ServiceDataTest
{
    public class IngredientDataTest
    {
        private readonly ITestOutputHelper _extraOutput;
        readonly private IIngredient _ingredientAccess;

        //readonly string _connectionString = "Server=Magnus-PC\\SQLEXPRESS; Integrated Security=true; Database=ServiceDB";
        readonly string _connectionString = "Server=localhost; Integrated Security=true; Database=x";

        public IngredientDataTest(ITestOutputHelper output)
        {
            _extraOutput = output;
            _ingredientAccess = new IngredientDatabaseAccess(_connectionString);
        }
        private byte[] GetTestImageBytes()
        {
            // Load test image data as a byte array
            // You can use libraries like System.Drawing or read from an image file
            // For simplicity, this example creates a small byte array
            return new byte[] { 0x12, 0x34, 0x56, 0x78 };
        }

        [Fact]
        public void TestCreateIngredient()
        {
            //Arrange
            Ingredient ing1 = new Ingredient("Salat", 10, GetTestImageBytes()); //creates object

            //Act
            int insertedId = _ingredientAccess.CreateIngredient(ing1); //Creates object and inserts into database and returns ID

            //Assert
            Assert.True(insertedId > 0); //Asserts true if an Id was returned

            //Cleanup
            _ingredientAccess.DeleteIngredientById(insertedId); //Deletes as cleanup
        }
        [Fact]
        public void TestDeleteIngredientById()
        {
            // Arrange
            Ingredient ing1 = new Ingredient("Salat", 10, GetTestImageBytes()); //creates object
            int insertedId = _ingredientAccess.CreateIngredient(ing1); // Inserts object to Database

            // Act
            bool isDeleted = _ingredientAccess.DeleteIngredientById(insertedId);//Deletes object

            // Assert
            Assert.True(isDeleted); //Asserts true if object is deleted.

        }
        [Fact]
        public void TestGetAllIngredients()
        {
            // Arrange
            Ingredient ing1 = new Ingredient("Salat", 10, GetTestImageBytes()); //creates object
            int insertedId = _ingredientAccess.CreateIngredient(ing1); // Inserts object to Database

            // Act
            List<Ingredient> readIngredients = _ingredientAccess.GetAllIngredients();
            bool IngredientsWereRead = (readIngredients.Count > 0);
            // Print additional output
            _extraOutput.WriteLine("Number of Ingredients: " + readIngredients.Count);

            // Assert
            Assert.True(IngredientsWereRead);

            //Cleanup
            _ingredientAccess.DeleteIngredientById(insertedId);
        }
        [Fact]
        public void TestUpdateIngredient()
        {
            // Arrange
            Ingredient ing1 = new Ingredient("Salat", 10, GetTestImageBytes()); //creates object
            int insertedId = _ingredientAccess.CreateIngredient(ing1); // Inserts object to Database

            // Modify the Ingredient object
            Ingredient updatedIng = new Ingredient(insertedId, "karl", 10, GetTestImageBytes());

            // Act
            bool isUpdated = _ingredientAccess.UpdateIngredientById(updatedIng);

            // Retrieve the updated Ingredient from the database
            Ingredient retrivedIng = _ingredientAccess.GetIngredientById(insertedId);

            // Assert
            Assert.True(isUpdated); //Assert true if update went through
            Assert.NotNull(retrivedIng); //Asserts true of the retrieved object is not null
            Assert.Equal(insertedId, retrivedIng.Id); //Asserts true if insertedID and retrivedId is the same
            Assert.Equal(retrivedIng.IngredientPrice, 10); //Asserts true if retrived parameter equals given parameter, 10 in this case. 

            //Cleanup
            _ingredientAccess.DeleteIngredientById(insertedId);

        }


    }
}