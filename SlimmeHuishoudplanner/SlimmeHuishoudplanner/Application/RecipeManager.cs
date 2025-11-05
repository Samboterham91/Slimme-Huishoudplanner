using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimmeHuishoudplanner.Domain;
using SlimmeHuishoudplanner.Persistence;


namespace SlimmeHuishoudplanner.Application
{
    public class RecipeManager
    {
        private readonly IDatabase _db;
        public RecipeManager(IDatabase database)
        {
            _db = database;
        }

        // Recipe toevoegen
        public Recipe AddRecipe(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Naam mag niet leeg zijn.");
            }
            var recipe = new Recipe
            {
                Name = name,
            };
            return _db.AddRecipe(recipe);
        }

        // Recipes opvragen
        public IEnumerable<Recipe> GetRecipes()
        {
            return _db.GetRecipes();
        }

        // Recipe opvragen op ID
        public Recipe? GetRecipeById(int id)
        {
            return _db.GetRecipeById(id);
        }

        // Recipe verwijderen
        public bool DeleteRecipe(int id)
        {
            return _db.DeleteRecipe(id);
        }

        // Recipe bijwerken; ingrediënten toevoegen
        public bool AddIngredient(int recipeId, string ingredient)
        {
            if (string.IsNullOrWhiteSpace(ingredient))
            {
                throw new ArgumentException("Ingrediënt mag niet leeg zijn.");
            }
            var recipe = _db.GetRecipeById(recipeId);
            if (recipe == null) return false;

            recipe.AddIngredient(ingredient);
            return true;
        }

        // Recipe bijwerken; ingrediënten verwijderen
        public bool RemoveIngredient(int recipeId, string ingredient)
        {
            var recipe = _db.GetRecipeById(recipeId);
            if (recipe == null) return false;

            recipe.RemoveIngredient(ingredient);
            return true;
        }
    }
}
