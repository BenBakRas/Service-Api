using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.ModelLayer
{
    public class Product
    {
        public int Id { get; set; }
        public string? ProductNumber { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public int? Barcode { get; set; }
        public List<Ingredient> ingredients { get; set; }

        //Empty Constructor
        public Product() { }

        //Constructor with parameters
        public Product(string productNumber, string description, double price, int barcode)
        {
            ProductNumber = productNumber;
            Description = description;
            Price = price;
            Barcode = barcode;

        }

        //Reuses constructor with Id
        public Product(int id, string productNumber, string description, double price, int barcode) : this(productNumber, description, price, barcode)
        {
            Id = id;
        }


    }
}
