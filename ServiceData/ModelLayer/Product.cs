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
        public decimal? BasePrice { get; set; }
        public int? Barcode { get; set; }
        public enum _Category {Burgere, Salater, Sides, Dips}
        public _Category Category { get; set; }
        public int? ProductGroup { get; set; }
        public String? Image {  get; set; }

        //Empty Constructor
        public Product() { }

        //Constructor with parameters
        public Product(string productNumber, string description, decimal basePrice, int barcode, _Category category, int productGroup)
        {
            ProductNumber = productNumber;
            Description = description;
            BasePrice = basePrice;
            Barcode = barcode;
            Category = category;
            ProductGroup = productGroup;
        }

        //Reuses constructor with Id
        public Product(int id, string productNumber, string description, decimal basePrice, int barcode, _Category category, int productGroup) : this(productNumber, description, basePrice, barcode, category, productGroup)
        {
            Id = id;
        }

        // Constructor with image
        public Product(int id, string productNumber, string description, decimal basePrice, int barcode, _Category category, int productGroup, string image) : this(id, productNumber, description, basePrice, barcode, category, productGroup)
        {
            Image = image;
        }

    }
}
