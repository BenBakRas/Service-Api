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
using ServiceData.DatabaseLayer.Interfaces;

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

            string insertString = "INSERT INTO Ingredient (name, ingredientPrice, Image) OUTPUT INSERTED.ID VALUES (@Name, @IngredientPrice, @Image)";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand createCommand = new SqlCommand(insertString, con))
            {
                SqlParameter aIngNameParam = new SqlParameter("@Name", anIngredient.Name);
                createCommand.Parameters.Add(aIngNameParam);

                SqlParameter aIngPrice = new SqlParameter("@IngredientPrice", anIngredient.IngredientPrice);
                createCommand.Parameters.Add(aIngPrice);

                // Add the Image parameter
                SqlParameter imageParam = new SqlParameter("@Image", anIngredient.Image);
                createCommand.Parameters.Add(imageParam);

                con.Open();

                // Execute the command and read the generated key (ID)
                insertedId = (int)createCommand.ExecuteScalar();
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
            string queryString = "SELECT Id, name, ingredientPrice, image FROM Ingredient";
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
            Ingredient foundIngredient = null;

            string queryString = "select Id, name, ingredientPrice, image from Ingredient where id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                // Prepare SQL
                SqlParameter idParam = new SqlParameter("@Id", id);
                readCommand.Parameters.Add(idParam);

                con.Open();
                // Execute read
                using (SqlDataReader ingredientsReader = readCommand.ExecuteReader())
                {
                    while (ingredientsReader.Read())
                    {
                        foundIngredient = GetIngFromReader(ingredientsReader);
                    }
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
            Ingredient foundIng = null;

            int tempID;
            decimal tempIngPrice;
            string tempIngName;
            byte[] tempImage;

            // Fetch values
            tempID = ingredientsReader.GetInt32(ingredientsReader.GetOrdinal("Id"));
            tempIngPrice = ingredientsReader.GetDecimal(ingredientsReader.GetOrdinal("IngredientPrice"));
            tempIngName = ingredientsReader.GetString(ingredientsReader.GetOrdinal("Name"));

            // Check if the "Image" column is not null in the database
            int imageColumnIndex = ingredientsReader.GetOrdinal("Image");
            if (!ingredientsReader.IsDBNull(imageColumnIndex))
            {
                tempImage = (byte[])ingredientsReader[imageColumnIndex];
            }
            else
            {
                // Handle the case when the "Image" column is null in the database
                // You can set tempImage to null or an empty byte array as needed.
                tempImage = new byte[0]; // Empty byte array
            }

            // Create the Ingredient object
            foundIng = new Ingredient(tempID, tempIngName, tempIngPrice, tempImage);

            return foundIng;
        }

    }
}