using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.ModelLayer
{
    public class OrderlineGroup
    {
        public Product? Product { get; set; }
        public OrderLine? Orderline { get; set; }

        public Combo? Combo { get; set; }
        //Empty constructor
        public OrderlineGroup() { }

        //Constructor with parameteter xecept orderline
        public OrderlineGroup(Product product)
        {
            Product = product;
           
        }
        //Constructor with orderline
        public OrderlineGroup(Product product, OrderLine orderLine) : this (product)
        {
            Orderline = orderLine;
        }


    }
}
