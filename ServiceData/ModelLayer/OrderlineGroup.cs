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
        public OrderlineGroup(Product product, Combo combo)
        {
            Product = product;
            Combo = combo;
        }
        //Constructor with orderline
        public OrderlineGroup(Product product, Combo combo, OrderLine orderLine) : this (product, combo)
        {
            Orderline = orderLine;
        }


    }
}
