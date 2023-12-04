using ServiceData.DatabaseLayer;
using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace ServiceDataTest
{
    public class CusGrpDataTest
    {
        /*
        private readonly ITestOutputHelper _extraOutput;
        readonly private ICustomerGroup _cusGrp;

        //readonly string _connectionString = "Server=Magnus-PC\\SQLEXPRESS; Integrated Security=true; Database=ServiceDB";
        readonly string _connectionString = "Server=localhost; Integrated Security=true; Database=x";

        public CusGrpDataTest(ITestOutputHelper output)
        {
            _extraOutput = output;
            _cusGrp = new CustomerGroupDatabaseAccess(_connectionString);
        }

        [Fact]
        public void TestCreateCusGrp()
        {
            //Arrange
            CustomerGroup cusgrp = new CustomerGroup("Vinder");//Creates object

            //Act
            int insertedId = _cusGrp.CreateCustomerGroup(cusgrp); //Creates object and inserts into database and returns ID

            //Assert
            Assert.True(insertedId > 0); //Asserts true if an Id was returned

            //Cleanup
            _cusGrp.DeleteCustomerGroupById(insertedId); //Deletes as cleanup
        }
        [Fact]
        public void TestDeleteCusGrpById()
        {
            // Arrange
            CustomerGroup cusgrp = new CustomerGroup("Vinder"); //Creates object
            int insertedId = _cusGrp.CreateCustomerGroup(cusgrp); // Inserts object to Database

            // Act
            bool isDeleted = _cusGrp.DeleteCustomerGroupById(insertedId);//Deletes object

            // Assert
            Assert.True(isDeleted); //Asserts true if object is deleted.

        }
        [Fact]
        public void TestGetAllCusGrps()
        {
            // Arrange
            CustomerGroup cusgrp = new CustomerGroup("Vinder"); //Creates object
            int insertedId = _cusGrp.CreateCustomerGroup(cusgrp); // Inserts object to Database

            // Act
            List<CustomerGroup> readCusGrps = _cusGrp.GetAllCustomerGroup();
            bool IngredientsWereRead = (readCusGrps.Count > 0);
            // Print additional output
            _extraOutput.WriteLine("Number of Customer Groups: " + readCusGrps.Count);

            // Assert
            Assert.True(IngredientsWereRead);

            //Cleanup
            _cusGrp.DeleteCustomerGroupById(insertedId);
        }
        [Fact]
        public void TestUpdateIngredient()
        {
            // Arrange
            CustomerGroup cusgrp = new CustomerGroup("Vinder"); //Creates object
            int insertedId = _cusGrp.CreateCustomerGroup(cusgrp); // Inserts object to Database

            // Modify the Ingredient object
            CustomerGroup updatedcusgrp = new CustomerGroup(insertedId, "Taber");

            // Act
            bool isUpdated = _cusGrp.UpdateCustomerGroupById(updatedcusgrp);

            // Retrieve the updated Ingredient from the database
            CustomerGroup retrivedCusGrp = _cusGrp.GetCustomerGroupById(insertedId);

            // Assert
            Assert.True(isUpdated); //Assert true if update went through
            Assert.NotNull(retrivedCusGrp); //Asserts true of the retrieved object is not null
            Assert.Equal(insertedId, retrivedCusGrp.Id); //Asserts true if insertedID and retrivedId is the same

            //Cleanup
            _cusGrp.DeleteCustomerGroupById(insertedId);

        } */
    }
        
}

