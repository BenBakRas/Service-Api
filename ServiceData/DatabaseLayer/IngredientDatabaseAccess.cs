using ServiceData.ModelLayer;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Numerics;
using System.Data;

namespace ServiceData.DatabaseLayer
{
    public class IngredientDatabaseAccess : IIngredient
    {

        readonly string? _connectionString;

        public IngredientDatabaseAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CompanyConnection");
        }

        public IngredientDatabaseAccess(string inConnectionString)
        {
            _connectionString = inConnectionString;
        }


        public int CreateIngredient(Ingredient anIngredient)
        {
            int insertedId = -1;
            //
            string insertString = "insert into Ingredient(name, ingredientPrice) OUTPUT INSERTED.ID values(@Name, @IngredientPrice)";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand CreateCommand = new SqlCommand(insertString, con))
            {
                SqlParameter aIngNameParam = new("@Name", anIngredient.Name);
                CreateCommand.Parameters.Add(aIngNameParam);
                SqlParameter aIngPrice = new("IngredientPrice", anIngredient.IngredientPrice);
                CreateCommand.Parameters.Add(aIngPrice);
                con.Open();
                // Execute save and read generated key (ID)
                insertedId = (int)CreateCommand.ExecuteScalar();
            }
            return insertedId;
        }

        public bool DeleteIngredientById(int id)
        {
            bool isDeleted = false;
            //
            string deleteString = "DELETE FROM Ingredient WHERE Id = @Id";
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

        public List<Ingredient> GetAllIngredients()
        {
            List<Ingredient> foundIngredients;
            Ingredient readIng;
            //
            string queryString = "SELECT Id, laneNumber FROM Lane";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                con.Open();
                // Execute read
                SqlDataReader ingredientsReader = readCommand.ExecuteReader();
                // Collect data
                foundIngredients = new List<Ingredient>();
                while (ingredientsReader.Read())
                {
                    readIng = GetIngFromReader(ingredientsReader);
                    foundIngredients.Add(readIng);
                }
            }
            return foundIngredients;
        }

        public Ingredient GetIngredientById(int id)
        {
            Ingredient foundIngredient;
            //
            string queryString = "select Id, name, ingredientPrice from Ingredient where id = @Id";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                //Prepare SQL
                SqlParameter idParam = new SqlParameter("@Id", id);
                readCommand.Parameters.Add(idParam);
                //
                con.Open();
                //Execute reead
                SqlDataReader ingredientsReader = readCommand.ExecuteReader();
                foundIngredient = new Ingredient();
                while (ingredientsReader.Read())
                {
                    foundIngredient = GetIngFromReader(ingredientsReader);
                }
            }
            return foundIngredient;
        }

        public bool UpdateIngredientById(Ingredient ingredientToUpdate)
        {
            bool isUpdated = false;
            string updateString = "UPDATE Ingredient SET name = @Name, ingredientPrice = @IngredientPrice WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand updateCommand = new SqlCommand(updateString, con))
            {
                updateCommand.Parameters.AddWithValue("@Id", ingredientToUpdate.Id);
                updateCommand.Parameters.AddWithValue("@Name", ingredientToUpdate.Name);
                updateCommand.Parameters.AddWithValue("@IngredientPrice", ingredientToUpdate.IngredientPrice);

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

        private Ingredient GetIngFromReader(SqlDataReader ingredientsReader)
        {
            Ingredient foundIng;
            int tempID, tempIngPrice;
            string tempIngName;
            //fetch values
            tempID = ingredientsReader.GetInt32(ingredientsReader.GetOrdinal("Id"));
            tempIngPrice = ingredientsReader.GetInt32(ingredientsReader.GetOrdinal("IngredientPrice"));
            tempIngName = ingredientsReader.GetString(ingredientsReader.GetOrdinal("Name"));
            //Create ingredient object
            foundIng = new Ingredient(tempID, tempIngName, tempIngPrice);

            return foundIng;
        }

    }
}
