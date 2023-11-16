﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.ModelLayer;
using System.Data.SqlClient;

namespace ServiceData.DatabaseLayer
{
    public class IngredientOrderlineDatabaseAccess : IIngredientOrderline
    {
        readonly string? _connectionString;

        public IngredientOrderlineDatabaseAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CompanyConnection");
        }

        public IngredientOrderlineDatabaseAccess(string inConnectionString)
        {
            _connectionString = inConnectionString;
        }

        public int CreateIngredientOrderline(IngredientOrderline ingredientOrderline)
        {
            int insertedId = -1;

            string insertString = "INSERT INTO IngredientOrderline(IngredientId, OrderlineId, Delta) " +
                "OUTPUT INSERTED.ID values(@IngredientId, @OrderlineId, @Delta)";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand createCommand = new SqlCommand(insertString, con))
            {
                createCommand.Parameters.AddWithValue("@IngredientId", ingredientOrderline.IngredientId);
                createCommand.Parameters.AddWithValue("@OrderlineId", ingredientOrderline.OrderlineId);
                createCommand.Parameters.AddWithValue("@Delta", ingredientOrderline.Delta);

                con.Open();

                insertedId = (int)createCommand.ExecuteScalar();
            }

            return insertedId;
        }

        public bool DeleteIngredientOrderlineById(int id)
        {
            bool isDeleted = false;

            string deleteString = "DELETE FROM IngredientOrderline WHERE Id = @Id";

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

        public List<IngredientOrderline> GetAllIngredientOrderlines()
        {
            List<IngredientOrderline> foundIngredientOrderlines = new List<IngredientOrderline>();
            IngredientOrderline readIngredientOrderline;

            string queryString = "SELECT * FROM IngredientOrderline";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                con.Open();

                SqlDataReader ingredientOrderlineReader = readCommand.ExecuteReader();

                while (ingredientOrderlineReader.Read())
                {
                    readIngredientOrderline = GetIngredientOrderlineFromReader(ingredientOrderlineReader);
                    foundIngredientOrderlines.Add(readIngredientOrderline);
                }
            }

            return foundIngredientOrderlines;
        }

        public IngredientOrderline GetIngredientOrderlineById(int id)
        {
            IngredientOrderline foundIngredientOrderline;

            string queryString = "SELECT * FROM IngredientOrderline WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                readCommand.Parameters.AddWithValue("@Id", id);

                con.Open();

                SqlDataReader ingredientOrderlineReader = readCommand.ExecuteReader();

                foundIngredientOrderline = new IngredientOrderline();

                while (ingredientOrderlineReader.Read())
                {
                    foundIngredientOrderline = GetIngredientOrderlineFromReader(ingredientOrderlineReader);
                }
            }

            return foundIngredientOrderline;
        }

        public bool UpdateIngredientOrderlineById(IngredientOrderline ingredientOrderlineToUpdate)
        {
            bool isUpdated = false;

            string updateString = "UPDATE IngredientOrderline SET IngredientId = @IngredientId, OrderlineId = @OrderlineId, Delta = @Delta " +
                                  "WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand updateCommand = new SqlCommand(updateString, con))
            {
                updateCommand.Parameters.AddWithValue("@IngredientId", ingredientOrderlineToUpdate.IngredientId);
                updateCommand.Parameters.AddWithValue("@OrderlineId", ingredientOrderlineToUpdate.OrderlineId);
                updateCommand.Parameters.AddWithValue("@Delta", ingredientOrderlineToUpdate.Delta);
                updateCommand.Parameters.AddWithValue("@Id", ingredientOrderlineToUpdate.Id);

                con.Open();

                int rowsAffected = updateCommand.ExecuteNonQuery();

                isUpdated = (rowsAffected > 0);
            }

            return isUpdated;
        }

        private IngredientOrderline GetIngredientOrderlineFromReader(SqlDataReader ingredientOrderlineReader)
        {
            IngredientOrderline foundIngredientOrderline;

            int readerId = ingredientOrderlineReader.GetInt32(ingredientOrderlineReader.GetOrdinal("Id"));
            int readerIngredientId = ingredientOrderlineReader.GetInt32(ingredientOrderlineReader.GetOrdinal("IngredientId"));
            int readerOrderlineId = ingredientOrderlineReader.GetInt32(ingredientOrderlineReader.GetOrdinal("OrderlineId"));
            int readerDelta = ingredientOrderlineReader.GetInt32(ingredientOrderlineReader.GetOrdinal("Delta"));

            foundIngredientOrderline = new IngredientOrderline(readerIngredientId, readerOrderlineId, readerDelta)
            {
                Id = readerId
            };

            return foundIngredientOrderline;
        }
    }
}
