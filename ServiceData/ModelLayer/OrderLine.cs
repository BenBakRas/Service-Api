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
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public int OrderlineGroupId { get; set; }  

        //Empty Constructor
        public OrderLine() { }

        //Constructor with parameters
        public OrderLine(int quantity, int orderId, int orderlineGroupId)
        {
          
            Quantity = quantity;
            OrderId = orderId;
            OrderlineGroupId = orderlineGroupId;
        }
        //Constructor with Id
        public OrderLine(int id, int quantity, int orderId, int orderlineGroupId) : this (quantity, orderId, orderlineGroupId)
        {
            Id = id;
        }
    }
}
