using ServiceData.ModelLayer;
using ServiceData.DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Xunit;
using System.Numerics;

namespace ServiceDataTest
{
    public class IngredientDataTest
    {
        private readonly ITestOutputHelper _extraOutput;
        readonly private IIngredient _ingredientAccess;

        readonly string _connectionString = "Server=localhost; Integrated Security=true; Database=x";

        public IngredientDataTest(ITestOutputHelper output)
        {
            _extraOutput = output;
            _ingredientAccess = new IngredientDatabaseAccess(_connectionString);
        }
        [Fact]
        public void TestCreateIngredient()
        {
            //Arrange
            Ingredient ing1= new Ingredient("Salat", 10.00); //creates object

            //Act
            int insertedId = _ingredientAccess.CreateIngredient(ing1); //Creates object and inserts into database and returns ID

            //Assert
            Assert.IsTrue(insertedId > 0); //Asserts true if an Id was returned

            //Cleanup
            _ingredientAccess.DeleteIngredientById(insertedId); //Deletes as cleanup
        }

        public void TestDeleteIngredientById() 
        {
            // Arrange
            Ingredient ing1 = new Ingredient("Salat", 10.00); //Creates object
            int insertedId = _ingredientAccess.CreateIngredient(ing1); // Inserts object to Database

            // Act
            bool isDeleted = _ingredientAccess.DeleteIngredientById(insertedId);//Deletes object

            // Assert
            Assert.IsTrue(isDeleted);//Asserts true if object is deleted.

        }

        public void TestGetAllIngredients()
        {
            // Arrange

            // Act
            List<Ingredient> readIngredients = _ingredientAccess.GetAllIngredients();
            bool IngredientsWereRead = (readIngredients.Count > 0);
            // Print additional output
            _extraOutput.WriteLine("Number of Ingredients: " + readIngredients.Count);

            // Assert
            Assert.IsTrue(IngredientsWereRead);
        }

        public void TestUpdateIngredient()
        {
            // Arrange
            Ingredient ing1 = new Ingredient("Salat", 10.00); //Creates object
            int insertedId = _ingredientAccess.CreateIngredient(ing1); // Inserts object to Database

            // Modify the Lane object
            Ingredient updatedIng = new Ingredient(insertedId, "karl", 10);

            // Act
            bool isUpdated = _ingredientAccess.UpdateIngredientById(updatedIng);

            // Retrieve the updated Lane from the database
            Ingredient retrivedIng = _ingredientAccess.GetIngredientById(insertedId);

            // Assert
            Assert.IsTrue(isUpdated); //Assert true if update went through
            Assert.IsNotNull(retrivedIng); //Asserts true of the retrieved object is not null
            Assert.Equals(insertedId, retrivedIng.Id); //Asserts true if insertedID and retrivedId is the same
            Assert.Equals(retrivedIng.IngredientPrice, 10); //Asserts true if retrived parameter equals given parameter, 10 in this case. 

            //Cleanup
            _ingredientAccess.DeleteIngredientById(insertedId);

        }


    }
}
