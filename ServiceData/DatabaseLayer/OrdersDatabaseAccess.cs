using Microsoft.Extensions.Configuration;
using ServiceData.ModelLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.DatabaseLayer
{
    public class OrdersDatabaseAccess : IOrders
    {
        readonly string? _connectionString;

        public OrdersDatabaseAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CompanyConnection");
        }

        public OrdersDatabaseAccess(string inConnectionString)
        {
            _connectionString = inConnectionString;
        }

        public int CreateOrder(Orders aOrder)
        {
            int insertedId = -1;
            //
            string insertString = "INSERT INTO  Orders(OrderNumber, DateTime, TotalPrice, ShopID) OUTPUT INSERTED.ID " +
                "values(@OrderNumber, @DateTime, @TotalPrice, @ShopID)";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand CreateCommand = new SqlCommand(insertString, con))
            {
                //Add values
                SqlParameter aOrderNumberParam = new("OrderNumber", aOrder.OrderNumber);
                CreateCommand.Parameters.Add(aOrderNumberParam);

                SqlParameter aOrderDateTime = new("DateTime", DateTime.Now);
                CreateCommand.Parameters.Add(aOrderDateTime);

                SqlParameter aTotalPriceParam = new("TotalPrice", aOrder.TotalPrice);   
                CreateCommand.Parameters.Add(aOrderNumberParam);

                SqlParameter aOrderShopIdParam = new("ShopID", aOrder.Shop.Id);
                CreateCommand.Parameters.Add(aOrderShopIdParam);

                con.Open();
                // Execute save and read generated key (ID)
                insertedId = (int)CreateCommand.ExecuteScalar();
            }
            return insertedId;
        }

        public bool DeleteOrderById(int id)
        {
            bool isDeleted = false;
            //
            string deleteString = "DELETE FROM Orders WHERE Id = @Id";
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

        public List<Orders> GetAllOrders()
        {
            List<Orders> foundOrders;
            Orders readOrder;
            //
            string queryString = "SELECT * FROM Orders";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                con.Open();
                // Execute read
                SqlDataReader ordersReader = readCommand.ExecuteReader();
                // Collect data
                foundOrders = new List<Orders>();
                while (ordersReader.Read())
                {
                    readOrder = GetOrdersFromReader(ordersReader);
                    foundOrders.Add(readOrder);
                }
            }
            return foundOrders;
        }

        public Orders GetOrderById(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateOrderById(Product orderToUpdate)
        {
            throw new NotImplementedException();
        }

        private Orders GetOrdersFromReader(SqlDataReader ordersReader)
        {
            Orders foundOrder;

            //fetch values
            int readerId = ordersReader.GetInt32(ordersReader.GetOrdinal("Id"));
            int readerOrderNumber = ordersReader.GetInt32(ordersReader.GetOrdinal("Id"));
            DateTime readerDateTime = ordersReader.GetDateTime(ordersReader.GetOrdinal("DateTime"));
            double readerTotalPrice = ordersReader.GetDouble(ordersReader.GetOrdinal("TotalPrice"));
            int readerShopId = ordersReader.GetInt32(ordersReader.GetInt32(ordersReader.GetOrdinal("ShopID")));


            //Create orders object
            foundOrder = new Orders(readerId, readerOrderNumber, readerDateTime, readerTotalPrice, readerShopId);


            return foundOrder;
        }
    }
}
