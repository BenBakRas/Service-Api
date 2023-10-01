using ServiceData.ModelLayer;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.DatabaseLayer
{
    public interface IIngredient
    {
        Ingredient GetIngredientById(int id);
        List<Ingredient> GetAllIngredients();
        int CreateIngredient(Ingredient anIngredient);
        bool DeleteIngredientById(int id);
        bool UpdateIngredientById(Ingredient ingredientToUpdate);


    }
}
