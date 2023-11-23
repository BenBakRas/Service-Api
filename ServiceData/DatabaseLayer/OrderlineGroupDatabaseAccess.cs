using Microsoft.Extensions.Configuration;
using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.ModelLayer;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ServiceData.DatabaseLayer
{
    public class OrderlineGroupDatabaseAccess : IOrderlineGroup
    {
        readonly string? _connectionString;

        public OrderlineGroupDatabaseAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Companyconnection");
        }

        public OrderlineGroupDatabaseAccess(string inConnectionString)
        {
            _connectionString = inConnectionString;
        }

        public async Task<int> CreateOrderlineGroup(OrderlineGroup orderlineGroup)
        {
            int insertedId = -1;
            string insertString = "INSERT INTO OrderlineGroup(Id, ProductId, OrderlineId, ComboId) VALUES(@Id, @ProductId, @OrderlineId, @ComboId); SELECT SCOPE_IDENTITY()";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand createCommand = new SqlCommand(insertString, con))
            {
                createCommand.Parameters.AddWithValue("@Id", orderlineGroup.Id);
                createCommand.Parameters.AddWithValue("@ProductId", orderlineGroup.ProductId);
                createCommand.Parameters.AddWithValue("@OrderlineId", orderlineGroup.OrderlineId);
                createCommand.Parameters.AddWithValue("@ComboId", orderlineGroup.ComboId);

                await con.OpenAsync();
                insertedId = Convert.ToInt32(await createCommand.ExecuteScalarAsync());
            }
            return insertedId;
        }

        public async Task<List<OrderlineGroup>> GetAllOrderlineGroups()
        {
            List<OrderlineGroup> foundOrderlineGroups = new List<OrderlineGroup>();

            string queryString = "SELECT Id, ProductId, OrderlineId, ComboId FROM OrderlineGroup";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                await con.OpenAsync();
                SqlDataReader orderlineGroupsReader = await readCommand.ExecuteReaderAsync();

                while (await orderlineGroupsReader.ReadAsync())
                {
                    OrderlineGroup readOrderlineGroup = GetOrderlineGroupFromReader(orderlineGroupsReader);
                    foundOrderlineGroups.Add(readOrderlineGroup);
                }
            }

            return foundOrderlineGroups;
        }

        public async Task<bool> DeleteOrderlineGroupById(int id)
        {
            bool isDeleted = false;
            string deleteString = "DELETE FROM OrderlineGroup WHERE Id = @Id";

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

        public async Task<OrderlineGroup> GetOrderlineGroupById(int id)
        {
            OrderlineGroup foundOrderlineGroup = null;
            string queryString = "SELECT Id, ProductId, OrderlineId, ComboId FROM OrderlineGroup WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                readCommand.Parameters.AddWithValue("@Id", id);

                await con.OpenAsync();
                SqlDataReader orderlineGroupsReader = await readCommand.ExecuteReaderAsync();

                while (await orderlineGroupsReader.ReadAsync())
                {
                    foundOrderlineGroup = GetOrderlineGroupFromReader(orderlineGroupsReader);
                }
            }

            return foundOrderlineGroup;
        }

        public async Task<bool> UpdateOrderlineGroupById(OrderlineGroup orderlineGroupToUpdate)
        {
            bool isUpdated = false;
            string updateString = "UPDATE OrderlineGroup SET ProductId = @ProductId, OrderlineId = @OrderlineId, ComboId = @ComboId WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand updateCommand = new SqlCommand(updateString, con))
            {
                updateCommand.Parameters.AddWithValue("@Id", orderlineGroupToUpdate.Id);
                updateCommand.Parameters.AddWithValue("@ProductId", orderlineGroupToUpdate.ProductId);
                updateCommand.Parameters.AddWithValue("@OrderlineId", orderlineGroupToUpdate.OrderlineId);
                updateCommand.Parameters.AddWithValue("@ComboId", orderlineGroupToUpdate.ComboId);

                await con.OpenAsync();
                int rowsAffected = await updateCommand.ExecuteNonQueryAsync();

                isUpdated = (rowsAffected > 0);
            }

            return isUpdated;
        }

        private OrderlineGroup GetOrderlineGroupFromReader(SqlDataReader orderlineGroupsReader)
        {
            int tempId, tempProductId, tempOrderlineId, tempComboId;

            tempId = orderlineGroupsReader.GetInt32(orderlineGroupsReader.GetOrdinal("Id"));
            tempProductId = orderlineGroupsReader.GetInt32(orderlineGroupsReader.GetOrdinal("ProductId"));
            tempOrderlineId = orderlineGroupsReader.GetInt32(orderlineGroupsReader.GetOrdinal("OrderlineId"));
            tempComboId = orderlineGroupsReader.GetInt32(orderlineGroupsReader.GetOrdinal("ComboId"));

            return new OrderlineGroup(tempId, tempProductId, tempOrderlineId, tempComboId);
        }
    }
}
