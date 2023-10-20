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
        public int CreateOrderLine(OrderLine aOrderLine)
        {
            int insertedId = -1;
            //
            string insertString = "INSERT INTO  OrderLine(OrderLinePrice, Quantity, OrderLineGroupID, OrderID) OUTPUT INSERTED.ID " +
                "values(@OrderLinePrice, @Quantity, @OrderLineGroupId, @OrderId)";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand CreateCommand = new SqlCommand(insertString, con))
            {
                //Add values
                SqlParameter aOrderLinePriceParam = new("OrderLinePrice", aOrderLine.OrderlinePrice);
                CreateCommand.Parameters.Add(aOrderLinePriceParam);

                SqlParameter aOrderLineQuantity = new("Quantity", aOrderLine.Quantity);
                CreateCommand.Parameters.Add(aOrderLineQuantity);

                SqlParameter aOrderLineGroupIdParam = new("OrderLineGroupID", aOrderLine.OrderlineGroup);
                CreateCommand.Parameters.Add(aOrderLineGroupIdParam);

                SqlParameter aOrderLineOrderIdParam = new("OrderID", aOrderLine.Orders);
                CreateCommand.Parameters.Add(aOrderLineOrderIdParam);

                con.Open();
                // Execute save and read generated key (ID)
                insertedId = (int)CreateCommand.ExecuteScalar();
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
            List<OrderLine> foundOrderLines;
            OrderLine readOrderLine;
            //
            string queryString = "SELECT * FROM OrderLine";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                con.Open();
                // Execute read
                SqlDataReader orderLineReader = readCommand.ExecuteReader();
                // Collect data
                foundOrderLines = new List<OrderLine>();
                while (orderLineReader.Read())
                {
                    readOrderLine = GetOrderLinesFromReader(orderLineReader);
                    foundOrderLines.Add(readOrderLine);
                }
            }
            return foundOrderLines;
        }

        public OrderLine GetOrderLinetById(int id)
        {
            OrderLine foundOrderLine;
            //
            string queryString = "SELECT * FROM OrderLine WHERE Id = @Id";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                //Prepare SQL
                SqlParameter idParam = new SqlParameter("@Id", id);
                readCommand.Parameters.Add(idParam);

                con.Open();
                //Execute read
                SqlDataReader orderLineReader = readCommand.ExecuteReader();
                foundOrderLine = new OrderLine();
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
            string updateString = "UPDATE OrderLine SET OrderLinePrice = @OrderLinePrice, Quantity = @Quantity, " +
                "OrderLineGroupID = @OrderLineGroupId, OrderID = @OrderId;";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand updateCommand = new SqlCommand(updateString, con))
            {
                updateCommand.Parameters.AddWithValue("@OrderLinePrice", orderLineToUpdate.OrderlinePrice);
                updateCommand.Parameters.AddWithValue("@Quantity", orderLineToUpdate.Quantity);
                updateCommand.Parameters.AddWithValue("@OrderLineGroupId", orderLineToUpdate.OrderLineGroupId);
                updateCommand.Parameters.AddWithValue("@OrderId", orderLineToUpdate.OrderId);

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

        private OrderLine GetOrderLinesFromReader(SqlDataReader orderLineReader)
        {
            OrderLine foundOrderLine;

            //fetch values
            int readerId = orderLineReader.GetInt32(orderLineReader.GetOrdinal("Id"));
            double readerOrderLinePrice = orderLineReader.GetDouble(orderLineReader.GetOrdinal("OrderLinePrice"));
            int readerQuantity = orderLineReader.GetInt32(orderLineReader.GetOrdinal("Quantity"));
            int readerOrderlineGroupId = orderLineReader.GetInt32(orderLineReader.GetOrdinal("OrderLineGroupID"));
            int readerOrderID = orderLineReader.GetInt32(orderLineReader.GetOrdinal("OrderID"));

            //Create orderline object
            foundOrderLine = new OrderLine(readerId, readerOrderLinePrice, readerQuantity, readerOrderlineGroupId, readerOrderID);

            return foundOrderLine;
        }
    }
}
