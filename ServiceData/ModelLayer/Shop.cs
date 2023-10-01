using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.ModelLayer
{
    public class Shop
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public enum _Type {Restuarant, FoodStand, CandyStore }
        public _Type Type { get; set; }

        //Empty Constructor
        public Shop() { }

        //Constructor with parameters
        public Shop(string name, string location, _Type type)
        {
            Name = name;
            Location = location;
            Type = type;
        }

        //Constructor with Id
        public Shop(int id, string name, string location, _Type type) : this(name, location, type)
        {
            Id = id;
        }

    }
}
