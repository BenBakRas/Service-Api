using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.ModelLayer
{
    public class Ingredient
    {
        public string? Name { get; set; }
        public double? IngredientPrice { get; set; }
        public int Id { get; set; }
        //Empty Constructor
        public Ingredient() { }

        //Constructor with parameters
        public Ingredient(string name, double ingredientPrice)
        {
            Name = name;
            IngredientPrice = ingredientPrice;
        }

        //reuses constructor with Id
        public Ingredient(int id, string name, double ingredientPrice) : this(name, ingredientPrice)
        {
            Id = id;
        }

    }
}
