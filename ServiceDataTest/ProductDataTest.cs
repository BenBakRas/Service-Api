using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using ServiceData.DatabaseLayer;
using ServiceData.ModelLayer;

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
        public void TestCreateProduct()
        {
            //Arrange
            Product prod1 = new Product("12", "Hamburger", 50, 212141, Product._Category.Burgere, 1); //creates object

            //Act
            int insertedId = _productAccess.CreateProduct(prod1); //Creates object and inserts into database and returns ID

            //Assert
            Assert.True(insertedId > 0); //Asserts true if an Id was returned

            //Cleanup
            _productAccess.DeleteProductById(insertedId); //Deletes as cleanup
        }
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
        
        
        /*[Fact]
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
            Assert.True(isUpdated); //Assert true if update went through
            Assert.NotNull(retrivedIng); //Asserts true of the retrieved object is not null
            Assert.Equal(insertedId, retrivedIng.Id); //Asserts true if insertedID and retrivedId is the same
            Assert.Equal(retrivedIng.IngredientPrice, 10); //Asserts true if retrived parameter equals given parameter, 10 in this case. 

            //Cleanup
            _ingredientAccess.DeleteIngredientById(insertedId);

        } */
    }
}