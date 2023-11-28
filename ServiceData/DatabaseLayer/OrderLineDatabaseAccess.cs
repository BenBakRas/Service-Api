using Microsoft.Extensions.Configuration;
using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.ModelLayer;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ServiceData.DatabaseLayer
{
    public class OrderLineDatabaseAccess : IOrderLine
    {
        readonly string? _connectionString;

        public OrderLineDatabaseAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CompanyConnection");
        }

        public OrderLineDatabaseAccess(string inConnectionString)
        {
            _connectionString = inConnectionString;
        }

        public async Task<int> CreateOrderLine(OrderLine orderLine)
        {
            int insertedId = -1;
            string insertString = "INSERT INTO OrderLine (OrderlinePrice, Quantity, OrderId, OrderlineGroupId) OUTPUT INSERTED.ID " +
                "VALUES (@OrderlinePrice, @Quantity, @OrderId, @OrderlineGroupId)";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand createCommand = new SqlCommand(insertString, con))
            {
                createCommand.Parameters.AddWithValue("@OrderlinePrice", orderLine.OrderlinePrice);
                createCommand.Parameters.AddWithValue("@Quantity", orderLine.Quantity);
                createCommand.Parameters.AddWithValue("@OrderId", orderLine.OrderId);
                createCommand.Parameters.AddWithValue("@OrderlineGroupId", orderLine.OrderlineGroupId);

                await con.OpenAsync();
                insertedId = (int)await createCommand.ExecuteScalarAsync();
            }

            return insertedId;
        }


        public async Task<bool> DeleteOrderLineById(int id)
        {
            bool isDeleted = false;
            string deleteString = "DELETE FROM OrderLine WHERE Id = @Id";

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

        public async Task<List<OrderLine>> GetAllOrderLines()
        {
            List<OrderLine> foundOrderLines = new List<OrderLine>();

            string queryString = "SELECT * FROM OrderLine";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                await con.OpenAsync();
                SqlDataReader orderLineReader = await readCommand.ExecuteReaderAsync();

                while (await orderLineReader.ReadAsync())
                {
                    OrderLine readOrderLine = GetOrderLinesFromReader(orderLineReader);
                    foundOrderLines.Add(readOrderLine);
                }
            }

            return foundOrderLines;
        }

        public async Task<OrderLine> GetOrderLineById(int id)
        {
            OrderLine foundOrderLine = new OrderLine();

            string queryString = "SELECT * FROM OrderLine WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                readCommand.Parameters.AddWithValue("@Id", id);

                await con.OpenAsync();
                SqlDataReader orderLineReader = await readCommand.ExecuteReaderAsync();

                while (await orderLineReader.ReadAsync())
                {
                    foundOrderLine = GetOrderLinesFromReader(orderLineReader);
                }
            }

            return foundOrderLine;
        }

        public async Task<bool> UpdateOrderLineById(OrderLine orderLineToUpdate)
        {
            bool isUpdated = false;
            string updateString = "UPDATE OrderLine SET OrderlinePrice = @OrderlinePrice, Quantity = @Quantity, OrderId = @OrderId, OrderlineGroupId = @OrderlineGroupId WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand updateCommand = new SqlCommand(updateString, con))
            {
                updateCommand.Parameters.AddWithValue("@OrderlinePrice", orderLineToUpdate.OrderlinePrice);
                updateCommand.Parameters.AddWithValue("@Quantity", orderLineToUpdate.Quantity);
                updateCommand.Parameters.AddWithValue("@OrderId", orderLineToUpdate.OrderId);
                updateCommand.Parameters.AddWithValue("@OrderlineGroupId", orderLineToUpdate.OrderlineGroupId);
                updateCommand.Parameters.AddWithValue("@Id", orderLineToUpdate.Id);

                await con.OpenAsync();

                int rowsAffected = await updateCommand.ExecuteNonQueryAsync();
                isUpdated = (rowsAffected > 0);
            }

            return isUpdated;
        }


        private OrderLine GetOrderLinesFromReader(SqlDataReader orderLineReader)
        {
            int readerId = orderLineReader.GetInt32(orderLineReader.GetOrdinal("Id"));
            decimal readerPrice = orderLineReader.GetDecimal(orderLineReader.GetOrdinal("OrderlinePrice"));
            int readerQuantity = orderLineReader.GetInt32(orderLineReader.GetOrdinal("Quantity"));
            int readerOrderId = orderLineReader.GetInt32(orderLineReader.GetOrdinal("OrderId"));
            int readerOrderlineGroupId = orderLineReader.GetInt32(orderLineReader.GetOrdinal("OrderlineGroupId"));

            OrderLine foundOrderLine = new OrderLine(readerId, readerPrice, readerQuantity, readerOrderId, readerOrderlineGroupId);
            return foundOrderLine;
        }
    }
}
