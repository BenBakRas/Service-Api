using Microsoft.Extensions.Configuration;
using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.ModelLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.DatabaseLayer
{
    public class DiscountDatabaseAccess : IDiscount
    {
        readonly string? _connectionString;

        public DiscountDatabaseAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CompanyConnection");
        }

        public DiscountDatabaseAccess(string inConnectionString)
        {
            _connectionString = inConnectionString;
        }

        public int CreateDiscount(Discount anDiscount)
        {
            int insertedId = -1;
            //
            string insertString = "insert into Discount(rate, productGroupId, customerGroupId) OUTPUT INSERTED.ID values(@Rate, @ProductGroupId, @CustomerGroupId)";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand CreateCommand = new SqlCommand(insertString, con))
            {
                SqlParameter aRate = new("@Rate", anDiscount.Rate);
                CreateCommand.Parameters.Add(aRate);
                SqlParameter aproductGroupId = new("@ProductGroupId", anDiscount.ProductGroupId);
                CreateCommand.Parameters.Add(aproductGroupId);
                SqlParameter acustomerGroupId = new("@CustomerGroupId", anDiscount.CustomerGroupId);
                CreateCommand.Parameters.Add(acustomerGroupId);
                con.Open();
                // Execute save and read generated key (ID)
                insertedId = (int)CreateCommand.ExecuteScalar();
            }
            return insertedId;
        }

        public bool DeleteDiscountById(int id)
        {
            bool isDeleted = false;
            //
            string deleteString = "DELETE FROM Discount WHERE Id = @Id";
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

        public List<Discount> GetAllDiscount()
        {
            List<Discount> foundDiscounts;
            Discount readDiscount;
            //
            string queryString = "SELECT Id, Rate, ProductGroupId, CustomerGroupId FROM Discount";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                con.Open();
                // Execute read
                SqlDataReader discountReader = readCommand.ExecuteReader();
                // Collect data
                foundDiscounts = new List<Discount>();
                while (discountReader.Read())
                {
                    readDiscount = GetDiscountFromReader(discountReader);
                    foundDiscounts.Add(readDiscount);
                }
            }
            return foundDiscounts;
        }

        public Discount GetDiscountById(int id)
        {
            Discount foundDiscount;
            //
            string queryString = "select Id, rate, productGroupId, customerGroupId from Discount where id = @Id";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                //Prepare SQL
                SqlParameter idParam = new SqlParameter("@Id", id);
                readCommand.Parameters.Add(idParam);
                //
                con.Open();
                //Execute reead
                SqlDataReader discountReader = readCommand.ExecuteReader();
                foundDiscount = new Discount();
                while (discountReader.Read())
                {
                    foundDiscount = GetDiscountFromReader(discountReader);
                }
            }
            return foundDiscount;
        }

        public bool UpdateDiscountById(Discount DiscountToUpdate)
        {
            bool isUpdated = false;
            string updateString = "UPDATE Discount SET rate = @Rate, productGroupId = @ProductGroupId, customerGroupId = @CustomerGroupId WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand updateCommand = new SqlCommand(updateString, con))
            {
                updateCommand.Parameters.AddWithValue("@Id", DiscountToUpdate.Id);
                updateCommand.Parameters.AddWithValue("@Rate", DiscountToUpdate.Rate);
                updateCommand.Parameters.AddWithValue("@ProductGroupId", DiscountToUpdate.ProductGroupId);
                updateCommand.Parameters.AddWithValue("@CustomerGroupId", DiscountToUpdate.CustomerGroupId);

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

        private Discount GetDiscountFromReader(SqlDataReader discountReader)
        {
            Discount foundDiscount;
            decimal tempRate;
            int tempID, tempProductGroupId, tempCustomerGroupId;
            //fetch values
            tempID = discountReader.GetInt32(discountReader.GetOrdinal("Id"));
            tempRate = discountReader.GetDecimal(discountReader.GetOrdinal("Rate"));
            tempProductGroupId = discountReader.GetInt32(discountReader.GetOrdinal("ProductGroupId"));
            tempCustomerGroupId = discountReader.GetInt32(discountReader.GetOrdinal("CustomerGroupId"));
            //Create ingredient object
            foundDiscount = new Discount(tempID, tempRate, tempProductGroupId, tempCustomerGroupId);


            return foundDiscount;
        }
    }
}
