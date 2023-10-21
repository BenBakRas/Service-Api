using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.ModelLayer
{
    public class Ingredient
    {
        public string Name { get; set; }
        public decimal IngredientPrice { get; set; }
        public byte[] Image { get; set; }
        public int Id { get; set; }
        //Empty Constructor
        public Ingredient() { }

        //Constructor with parameters
        public Ingredient(string name, decimal ingredientPrice, byte[] image)
        {
            Name = name;
            IngredientPrice = ingredientPrice;
            Image = image;
        }

        //reuses constructor with Id
        public Ingredient(int id, string name, decimal ingredientPrice, byte[] image) : this(name, ingredientPrice, image)
        {
            Id = id;
        }

    }
}