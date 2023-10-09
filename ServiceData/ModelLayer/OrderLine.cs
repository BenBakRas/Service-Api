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
        public Product? Product { get; set; }
        public Orders? Orders { get; set; }

        //Empty Constructor
        public OrderLine() { }

        //Constructor with parameters
        public OrderLine(double orderlinePrice, int quantity, Product product, Orders order)
        {
            OrderlinePrice = orderlinePrice;
            Quantity = quantity;
            Product = product;
            Orders = order;
        }
        //Constructor with Id
        public OrderLine(int id, double orderlinePrice, int quantity, Product product, Orders order) : this (orderlinePrice, quantity, product, order)
        {
            Id = id;
        }


    }
}
