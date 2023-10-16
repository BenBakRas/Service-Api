using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.ModelLayer
{
    public class Orders 
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public DateTime DateTime { get; set; }
        public double TotalPrice { get; set; }
        public int ShopId { get; set; }
        public Shop? Shop { get; set; }
        //Empty Constructor
        public Orders() { }

        //Constructor with parameters
        public Orders(int orderNumber, DateTime dateTime, double totalPrice, Shop shop)
        {
            OrderNumber = orderNumber;
            DateTime = dateTime;
            TotalPrice = totalPrice;
            Shop = shop;
        }

        //Constructor with Id
        public Orders(int id, int orderNumber, DateTime dateTime, double totalPrice, Shop shop) : this (orderNumber, dateTime, totalPrice, shop)
        {
            Id = id;
        }

        public Orders(int id, int orderNumber, DateTime dateTime, double totalPrice, int shopId)
        {
            Id = id;
            OrderNumber = orderNumber;
            DateTime = dateTime;
            TotalPrice = totalPrice;
            ShopId = shopId;
        }


    }
}
