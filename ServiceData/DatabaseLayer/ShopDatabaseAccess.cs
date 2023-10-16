using Microsoft.Extensions.Configuration;
using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.ModelLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.DatabaseLayer
{
    public class ShopDatabaseAccess : IShop
    {

        readonly string? _connectionString;

        public ShopDatabaseAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Companyconnection");
        }

        public ShopDatabaseAccess(string inConnectionString) 
        { 
            _connectionString = inConnectionString;
        
        }

        public int CreateShop(Shop anShop)
        {
            int insertedId = -1;
            //
            string insertString = "insert into Shop(Name, Location, Type) OUTPUT INSERTED.ID values(@Name, @Location, @Type)";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand CreateCommand = new SqlCommand(insertString, con))
            {
                SqlParameter aNameParam = new("@Name", anShop.Name);
                CreateCommand.Parameters.Add(aNameParam);

                SqlParameter aLocationParam = new("@Location", anShop.Location);
                CreateCommand.Parameters.Add(aLocationParam);

                SqlParameter aTypeParam = new("@Type", anShop.Type);
                CreateCommand.Parameters.Add(aTypeParam);
                con.Open();
                // Execute save and read generated key (ID)
                insertedId = (int)CreateCommand.ExecuteScalar();
            }
            return insertedId;
        }

        public bool DeleteShopById(int id)
        {
            bool isDeleted = false;
            //
            string deleteString = "DELETE FROM Shop WHERE Id = @Id";
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

        public List<Shop> GetAllShops()
        {
            List<Shop> foundShops;
            Shop readShop;
            //
            string queryString = "SELECT Id, name, location, type FROM Shop";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                con.Open();
                // Execute read
                SqlDataReader shopsReader = readCommand.ExecuteReader();
                // Collect data
                foundShops = new List<Shop>();
                while (shopsReader.Read())
                {
                    readShop = GetShopFromReader(shopsReader);
                    foundShops.Add(readShop);
                }
            }
            return foundShops;
        }

        public Shop GetShopById(int id)
        {
            Shop foundShop;
            //
            string queryString = "select Id, name, location, type from Shop where id = @Id";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                //Prepare SQL
                SqlParameter idParam = new SqlParameter("@Id", id);
                readCommand.Parameters.Add(idParam);
                //
                con.Open();
                //Execute reead
                SqlDataReader shopsReader = readCommand.ExecuteReader();
                foundShop = new Shop();
                while (shopsReader.Read())
                {
                    foundShop = GetShopFromReader(shopsReader);
                }
            }
            return foundShop;
        }

        public bool UpdateShopById(Shop ShopToUpdate)
        {
            bool isUpdated = false;
            string updateString = "UPDATE Shop SET name = @Name, location = @Location, type = @Type WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand updateCommand = new SqlCommand(updateString, con))
            {
                updateCommand.Parameters.AddWithValue("@Id", ShopToUpdate.Id);
                updateCommand.Parameters.AddWithValue("@Name", ShopToUpdate.Name);
                updateCommand.Parameters.AddWithValue("@Location", ShopToUpdate.Location);
                updateCommand.Parameters.AddWithValue("@Type", ShopToUpdate.Type);

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

        private Shop GetShopFromReader(SqlDataReader shopsReader)
        {
            Shop foundshop;
            int tempId;
            string tempName, tempLocation, tempType;
            bool tempEnum;
            

            tempId = shopsReader.GetInt32(shopsReader.GetOrdinal("Id"));
            tempName = shopsReader.GetString(shopsReader.GetOrdinal("Name"));
            tempLocation = shopsReader.GetString(shopsReader.GetOrdinal("Location"));
            tempType = shopsReader.GetString(shopsReader.GetOrdinal("Type"));
            tempEnum = Enum.TryParse(tempType, out Shop._Type enumType);

            foundshop = new Shop(tempId, tempName, tempLocation, enumType);


            return foundshop;
        }

    }
}
