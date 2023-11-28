using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.ModelLayer
{
    public class OrderLine
    {

        public int Id { get; set; }
        public decimal OrderlinePrice { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public int OrderlineGroupId { get; set; }  

        //Empty Constructor
        public OrderLine() { }

        //Constructor with parameters
        public OrderLine(decimal orderlinePrice, int quantity, int orderId, int orderlineGroupId)
        {
            OrderlinePrice = orderlinePrice;
            Quantity = quantity;
            OrderId = orderId;
            OrderlineGroupId = orderlineGroupId;
        }
        //Constructor with Id
        public OrderLine(int id, decimal orderlinePrice, int quantity, int orderId, int orderlineGroupId) : this (orderlinePrice, quantity, orderId, orderlineGroupId)
        {
            Id = id;
        }
    }
}
