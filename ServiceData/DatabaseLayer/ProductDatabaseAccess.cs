﻿using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.ModelLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ServiceData.DatabaseLayer
{
    public class ProductDatabaseAccess : IProduct
    {
        private readonly string? _connectionString;

        public ProductDatabaseAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CompanyConnection");
        }

        public ProductDatabaseAccess(string inConnectionString)
        {
            _connectionString = inConnectionString;
        }

        public async Task<int> CreateProduct(Product product)
        {
            int insertedId = -1;
            string insertString = "INSERT INTO Product(ProductNumber, Description, BasePrice, Barcode, Category, ProductGroupID) " +
                "OUTPUT INSERTED.ID values(@Productnumber, @Description, @BasePrice, @Barcode, @Category, @ProductGroupID)";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand CreateCommand = new SqlCommand(insertString, con))
            {
                CreateCommand.Parameters.AddWithValue("@Productnumber", product.ProductNumber);
                CreateCommand.Parameters.AddWithValue("@Description", product.Description);
                CreateCommand.Parameters.AddWithValue("@BasePrice", product.BasePrice);
                CreateCommand.Parameters.AddWithValue("@Barcode", product.Barcode);
                CreateCommand.Parameters.AddWithValue("@Category", product.Category);
                CreateCommand.Parameters.AddWithValue("@ProductGroupID", product.ProductGroup);

                await con.OpenAsync();
                insertedId = (int)await CreateCommand.ExecuteScalarAsync();
            }

            return insertedId;
        }

        public async Task<bool> DeleteProductById(int id)
        {
            bool isDeleted = false;
            string deleteString = "DELETE FROM Product WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand deleteCommand = new SqlCommand(deleteString, con))
            {
                deleteCommand.Parameters.AddWithValue("@Id", id);

                await con.OpenAsync();
                int rowsAffected = await deleteCommand.ExecuteNonQueryAsync();

                isDeleted = (rowsAffected > 0);
            }

            return isDeleted;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            List<Product> foundProducts = new List<Product>();
            string queryString = "SELECT * FROM Product";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                await con.OpenAsync();
                using (SqlDataReader productReader = await readCommand.ExecuteReaderAsync())
                {
                    while (await productReader.ReadAsync())
                    {
                        Product readProduct = GetProductFromReader(productReader);
                        foundProducts.Add(readProduct);
                    }
                }
            }

            return foundProducts;
        }

        public async Task<Product> GetProductById(int id)
        {
            Product foundProduct = null;
            string queryString = "SELECT * FROM Product WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                readCommand.Parameters.AddWithValue("@Id", id);

                await con.OpenAsync();

                using (SqlDataReader productReader = await readCommand.ExecuteReaderAsync())
                {
                    while (await productReader.ReadAsync())
                    {
                        foundProduct = GetProductFromReader(productReader);
                    }
                }
            }

            return foundProduct;
        }

        public async Task<bool> UpdateProductById(Product productToUpdate)
        {
            bool isUpdated = false;
            string updateString = "UPDATE Product SET ProductNumber = @ProductNumber, Description = @Description, BasePrice = @BasePrice, " +
                "Barcode = @Barcode, Category = @Category, ProductGroupID = @ProductGroupID WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand updateCommand = new SqlCommand(updateString, con))
            {
                updateCommand.Parameters.AddWithValue("@ProductNumber", productToUpdate.ProductNumber);
                updateCommand.Parameters.AddWithValue("@Description", productToUpdate.Description);
                updateCommand.Parameters.AddWithValue("@BasePrice", productToUpdate.BasePrice);
                updateCommand.Parameters.AddWithValue("@Barcode", productToUpdate.Barcode);
                updateCommand.Parameters.AddWithValue("@Category", productToUpdate.Category);
                updateCommand.Parameters.AddWithValue("@ProductGroupID", productToUpdate.ProductGroup);
                updateCommand.Parameters.AddWithValue("@Id", productToUpdate.Id);

                await con.OpenAsync();
                int rowsAffected = await updateCommand.ExecuteNonQueryAsync();

                isUpdated = rowsAffected > 0;
            }

            return isUpdated;
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
            string readerImage;

            //Fetch values
            readerID = productReader.GetInt32(productReader.GetOrdinal("Id"));
            readerProductNumber = productReader.GetString(productReader.GetOrdinal("ProductNumber"));
            readerBarcode = productReader.GetInt32(productReader.GetOrdinal("Barcode"));
            readerDescription = productReader.GetString(productReader.GetOrdinal("Description"));
            readerBasePrice = productReader.GetDouble(productReader.GetOrdinal("BasePrice"));
            tempCategory = productReader.GetString(productReader.GetOrdinal("Category"));
            readerCategory = Enum.TryParse(tempCategory, out Product._Category categoryValue);
            readerProductGroup = productReader.GetInt32(productReader.GetOrdinal("ProductGroupID"));
            readerImage = productReader.GetString(productReader.GetOrdinal("Image"));
            

            //Create product object
            foundProduct = new Product(readerID, readerProductNumber, readerDescription, readerBasePrice, readerBarcode, categoryValue, readerProductGroup, readerImage);

            return foundProduct;
        }

    }
}
