﻿using ServiceData.ModelLayer;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Numerics;
using System.Data;
using ServiceData.DatabaseLayer.Interfaces;

namespace ServiceData.DatabaseLayer
{
    public class ProductDatabaseAccess : IProduct
    {

        readonly string? _connectionString;

        public ProductDatabaseAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CompanyConnection");
        }

            public ProductDatabaseAccess(string inConnectionString)
            {
                _connectionString = inConnectionString;
            }

        public int CreateProduct(Product product)
        {
            int insertedId = -1;
            //SQL string
            string insertString = "INSERT INTO Product(ProductNumber, Description, BasePrice, Barcode, Category, ProductGroupID) " +
                "OUTPUT INSERTED.ID values(@Productnumber, @Description, @BasePrice, @Barcode, @Category, @ProductGroupID)";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand CreateCommand = new SqlCommand(insertString, con))
            {
                //Preparing and adding parameters
                SqlParameter productNumberParam = new("@Productnumber", product.ProductNumber);
                CreateCommand.Parameters.Add(productNumberParam);

                SqlParameter productDescriptionParam = new("@Description", product.Description);
                CreateCommand.Parameters.Add(productDescriptionParam);

                SqlParameter productPriceParam = new("@BasePrice", product.BasePrice);
                CreateCommand.Parameters.Add(productPriceParam);

                SqlParameter productBarcodeParam = new("@Barcode", product.Barcode);
                CreateCommand.Parameters.Add(productBarcodeParam);

                SqlParameter productCategoryParam = new("@Category", product.Category);
                CreateCommand.Parameters.Add(productCategoryParam);

                SqlParameter productProdGroup = new("@ProductGroupID", product.ProductGroup);
                CreateCommand.Parameters.Add(productProdGroup);

                con.Open();
                // Execute save and read generated key(ID)
                insertedId = (int)CreateCommand.ExecuteScalar();
            }
            // Return true or false
            return insertedId;
        }

        public bool DeleteProductById(int id)
        {
            bool isDeleted = false;
            //
            string deleteString = "DELETE FROM Product WHERE Id = @Id";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand deleteCommand = new SqlCommand(deleteString, con))
            {
                deleteCommand.Parameters.AddWithValue("@Id", id);

                con.Open();
                int rowsAffected = deleteCommand.ExecuteNonQuery();

                isDeleted = (rowsAffected > 0);
            }

            return isDeleted;
        }

        public List<Product> GetAllProducts()
        {
            List<Product> foundProducts;
            Product readProduct;
            //Query
            string queryString = "SELECT * FROM Product";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                con.Open();
                // Execute read
                SqlDataReader productReader = readCommand.ExecuteReader();
                // Collect data
                foundProducts = new List<Product>();
                while (productReader.Read())
                {
                    readProduct = GetProductFromReader(productReader);
                    foundProducts.Add(readProduct);
                }
            }
            return foundProducts;
        }


        public Product GetProductById(int id)
        {
            Product foundProduct;
            //
            string queryString = "SELECT * FROM Product WHERE Id = @Id";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                //Prepare SQL
                SqlParameter idParam = new SqlParameter("@Id", id);
                readCommand.Parameters.Add(idParam);
                //
                con.Open();
                //Execute reead
                SqlDataReader productReader = readCommand.ExecuteReader();
                foundProduct = new Product();
                while (productReader.Read())
                {
                    foundProduct = GetProductFromReader(productReader);
                }
            }
            return foundProduct;
        }

        public bool UpdateProductById(Product productToUpdate)
        {
            bool isUpdated = false;
            string updateString = "UPDATE Product SET ProductNumber = @ProductNumber, Description = @Description, BasePrice = @BasePrice, " +
                "Barcode = @Barcode, Category = @Category, ProductGroupID = @ProductGroupID";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand updateCommand = new SqlCommand(updateString, con))
            {
                updateCommand.Parameters.AddWithValue("@ProductNumber", productToUpdate.ProductNumber);
                updateCommand.Parameters.AddWithValue("@Description", productToUpdate.Description);
                updateCommand.Parameters.AddWithValue("@BasePrice", productToUpdate.BasePrice);
                updateCommand.Parameters.AddWithValue("@Barcode", productToUpdate.Barcode);
                updateCommand.Parameters.AddWithValue("@Category", productToUpdate.Category);
                updateCommand.Parameters.AddWithValue("@ProductGroupID", productToUpdate.ProductGroup);

                con.Open();
                int rowsAffected = updateCommand.ExecuteNonQuery();

                if (isUpdated = (rowsAffected > 0))
                {
                    return isUpdated;
                }
                else
                {
                    return false;
                }
            }
        }


        private Product GetProductFromReader(SqlDataReader productReader)
        {
            Product foundProduct;
            int readerID;
            string readerProductNumber;
            string readerDescription;
            double readerBasePrice;
            int readerBarcode;
            string tempCategory;
            bool readerCategory;
            int readerProductGroup;

            //Fetch values
            readerID = productReader.GetInt32(productReader.GetOrdinal("Id"));
            readerProductNumber = productReader.GetString(productReader.GetOrdinal("ProductNumber"));
            readerBarcode = productReader.GetInt32(productReader.GetOrdinal("Barcode"));
            readerDescription = productReader.GetString(productReader.GetOrdinal("Description"));
            readerBasePrice = productReader.GetDouble(productReader.GetOrdinal("BasePrice"));
            tempCategory = productReader.GetString(productReader.GetOrdinal("Category"));
            readerCategory = Enum.TryParse(tempCategory, out Product._Category categoryValue);
            readerProductGroup = productReader.GetInt32(productReader.GetOrdinal("ProductGroupID"));


            //Create product object
            foundProduct = new Product(readerID, readerProductNumber, readerDescription, readerBasePrice, readerBarcode, categoryValue, readerProductGroup);

            return foundProduct;
        }

    }
}
