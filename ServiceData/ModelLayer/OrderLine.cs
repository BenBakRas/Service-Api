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
        public double OrderlinePrice { get; set; }
        public int Quantity { get; set; }
        public OrderlineGroup? OrderlineGroup { get; set; }
        public Orders? Orders { get; set; }
        public int OrderLineGroupId { get; set; }
        public int OrderId { get; set; }

        //Empty Constructor
        public OrderLine() { }

        //Constructor with parameters
        public OrderLine(double orderlinePrice, int quantity, OrderlineGroup orderlineGrp , Orders order)
        {
            OrderlinePrice = orderlinePrice;
            Quantity = quantity;
            OrderlineGroup = orderlineGrp;
            Orders = order;
        }
        //Constructor with Id
        public OrderLine(int id, double orderlinePrice, int quantity, OrderlineGroup orderlineGrp, Orders order) : this (orderlinePrice, quantity, orderlineGrp, order)
        {
            Id = id;
        }

        public OrderLine(int id, double orderlinePrice, int quantity, int orderLineGroupId, int orderId)
        {
            Id = id;
            OrderlinePrice = orderlinePrice;
            Quantity = quantity;
            OrderLineGroupId = orderLineGroupId;
            OrderId = orderId;
        }


    }
}
