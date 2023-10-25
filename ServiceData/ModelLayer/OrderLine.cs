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
        public Orders? Orders { get; set; }
        public int OrderId { get; set; }

        //Empty Constructor
        public OrderLine() { }

        //Constructor with parameters
        public OrderLine(decimal orderlinePrice, int quantity, Orders order)
        {
            OrderlinePrice = orderlinePrice;
            Quantity = quantity;
            Orders = order;
        }
        //Constructor with Id
        public OrderLine(int id, decimal orderlinePrice, int quantity, Orders order) : this (orderlinePrice, quantity, order)
        {
            Id = id;
        }

        //Constructor with OrderID
        public OrderLine(int id, decimal orderlinePrice, int quantity, int orderId)
        {
            Id = id;
            OrderlinePrice = orderlinePrice;
            Quantity = quantity;
            OrderId = orderId;
        }
        
        public OrderLine(decimal orderlinePrice, int quantity, int orderId)
        {
            OrderlinePrice = orderlinePrice;
            Quantity = quantity;
            OrderId = orderId;
        }

    }
}
