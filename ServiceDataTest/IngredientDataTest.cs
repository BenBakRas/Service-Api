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
    public class IngredientDataTest
    { /*
        private readonly ITestOutputHelper _extraOutput;
        private readonly IIngredient _ingredientAccess;
        private readonly string _connectionString = "Server=localhost; Integrated Security=true; Database=x";

        public IngredientDataTest(ITestOutputHelper output)
        {
            _extraOutput = output;
            _ingredientAccess = new IngredientDatabaseAccess(_connectionString);
        }

        [Fact]
        public async Task TestCreateIngredient()
        {
            // Arrange
            Ingredient ing1 = new Ingredient("Salat", 10, "Salat"); // creates object

            // Act
            int insertedId = await _ingredientAccess.CreateIngredient(ing1); // Creates object and inserts into the database and returns ID

            // Assert
            Assert.True(insertedId > 0); // Asserts true if an ID was returned

            // Cleanup
            await _ingredientAccess.DeleteIngredientById(insertedId); // Deletes as cleanup
        }

        [Fact]
        public async Task TestDeleteIngredientById()
        {
            // Arrange
            Ingredient ing1 = new Ingredient("Salat", 10, "Salat"); // creates object
            int insertedId = await _ingredientAccess.CreateIngredient(ing1); // Inserts object into the database

            // Act
            bool isDeleted = await _ingredientAccess.DeleteIngredientById(insertedId); // Deletes object

            // Assert
            Assert.True(isDeleted); // Asserts true if the object is deleted.
        }

        [Fact]
        public async Task TestGetAllIngredients()
        {
            // Arrange
            Ingredient ing1 = new Ingredient("Salat", 10, "salat"); // creates object
            int insertedId = await _ingredientAccess.CreateIngredient(ing1); // Inserts object into the database

            // Act
            List<Ingredient> readIngredients = await _ingredientAccess.GetAllIngredients();
            bool IngredientsWereRead = (readIngredients.Count > 0);
            // Print additional output
            _extraOutput.WriteLine("Number of Ingredients: " + readIngredients.Count);

            // Assert
            Assert.True(IngredientsWereRead);

            // Cleanup
            await _ingredientAccess.DeleteIngredientById(insertedId);
        }

        [Fact]
        public async Task TestUpdateIngredient()
        {
            // Arrange
            Ingredient ing1 = new Ingredient("Salat", 10, "Salat"); // creates object
            int insertedId = await _ingredientAccess.CreateIngredient(ing1); // Inserts object into the database

            // Modify the Ingredient object
            Ingredient updatedIng = new Ingredient(insertedId, "karl", 10, "Salat");

            // Act
            bool isUpdated = await _ingredientAccess.UpdateIngredientById(updatedIng);

            // Retrieve the updated Ingredient from the database
            Ingredient retrievedIng = await _ingredientAccess.GetIngredientById(insertedId);

            // Assert
            Assert.True(isUpdated); // Assert true if the update went through
            Assert.NotNull(retrievedIng); // Asserts true if the retrieved object is not null
            Assert.Equal(insertedId, retrievedIng.Id); // Asserts true if insertedID and retrievedId are the same
            Assert.Equal(retrievedIng.IngredientPrice, 10); // Asserts true if retrieved parameter equals the given parameter, 10 in this case.

            // Cleanup
            await _ingredientAccess.DeleteIngredientById(insertedId);
        } */
    }
}
