using Microsoft.Extensions.Configuration;
using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.ModelLayer;
using System.Collections.Generic;
using System.Data.SqlClient;

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

        public int CreateOrderLine(OrderLine orderLine)
        {
            int insertedId = -1;
            string insertString = "INSERT INTO OrderLine(Quantity, OrderId, OrderlineGroupId) OUTPUT INSERTED.ID " +
                "VALUES(@Quantity, @OrderId, @OrderlineGroupId)";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand createCommand = new SqlCommand(insertString, con))
            {
                createCommand.Parameters.AddWithValue("@Quantity", orderLine.Quantity);
                createCommand.Parameters.AddWithValue("@OrderId", orderLine.OrderId);
                createCommand.Parameters.AddWithValue("@OrderlineGroupId", orderLine.OrderlineGroupId);

                con.Open();
                insertedId = (int)createCommand.ExecuteScalar();
            }

            return insertedId;
        }

        public bool DeleteOrderLineById(int id)
        {
            bool isDeleted = false;
            string deleteString = "DELETE FROM OrderLine WHERE Id = @Id";

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

        public List<OrderLine> GetAllOrderLines()
        {
            List<OrderLine> foundOrderLines = new List<OrderLine>();

            string queryString = "SELECT * FROM OrderLine";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                con.Open();
                SqlDataReader orderLineReader = readCommand.ExecuteReader();

                while (orderLineReader.Read())
                {
                    OrderLine readOrderLine = GetOrderLinesFromReader(orderLineReader);
                    foundOrderLines.Add(readOrderLine);
                }
            }

            return foundOrderLines;
        }

        public OrderLine GetOrderLineById(int id)
        {
            OrderLine foundOrderLine = new OrderLine();

            string queryString = "SELECT * FROM OrderLine WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                readCommand.Parameters.AddWithValue("@Id", id);

                con.Open();
                SqlDataReader orderLineReader = readCommand.ExecuteReader();

                while (orderLineReader.Read())
                {
                    foundOrderLine = GetOrderLinesFromReader(orderLineReader);
                }
            }

            return foundOrderLine;
        }

        public bool UpdateOrderLineById(OrderLine orderLineToUpdate)
        {
            bool isUpdated = false;
            string updateString = "UPDATE OrderLine SET Quantity = @Quantity, OrderId = @OrderId, OrderlineGroupId = @OrderlineGroupId WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand updateCommand = new SqlCommand(updateString, con))
            {
                updateCommand.Parameters.AddWithValue("@Quantity", orderLineToUpdate.Quantity);
                updateCommand.Parameters.AddWithValue("@OrderId", orderLineToUpdate.OrderId);
                updateCommand.Parameters.AddWithValue("@OrderlineGroupId", orderLineToUpdate.OrderlineGroupId);
                updateCommand.Parameters.AddWithValue("@Id", orderLineToUpdate.Id);

                con.Open();

                int rowsAffected = updateCommand.ExecuteNonQuery();
                isUpdated = (rowsAffected > 0);
            }

            return isUpdated;
        }

        private OrderLine GetOrderLinesFromReader(SqlDataReader orderLineReader)
        {
            int readerId = orderLineReader.GetInt32(orderLineReader.GetOrdinal("Id"));
            int readerQuantity = orderLineReader.GetInt32(orderLineReader.GetOrdinal("Quantity"));
            int readerOrderId = orderLineReader.GetInt32(orderLineReader.GetOrdinal("OrderId"));
            int readerOrderlineGroupId = orderLineReader.GetInt32(orderLineReader.GetOrdinal("OrderlineGroupId"));


            OrderLine foundOrderLine = new OrderLine(readerId, readerQuantity, readerOrderId, readerOrderlineGroupId);
            return foundOrderLine;
        }
    }
}
