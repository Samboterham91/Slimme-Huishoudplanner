using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SlimmeHuishoudplanner.Application;
using SlimmeHuishoudplanner.Persistence;
using SlimmeHuishoudplanner.Domain;

namespace SlimmeHuishoudplanner.Tests.Application
{
    public class RecipeManagerTests
    {
        [Fact]
        public void AddRecipeSucceeds()
        {
            // Arrange
            var db = new InMemoryDatabase();
            var recipeManager = new RecipeManager(db);
            // Act
            var recipe = recipeManager.AddRecipe("Test Recipe");
            // Assert
            Assert.NotNull(recipe);
            Assert.True(recipe.Id > 0);
            Assert.Equal("Test Recipe", recipe.Name);
            Assert.Empty(recipe.Ingredients);
        }

        [Fact]
        public void AddRecipeFails_WhenNameEmpty()
        {
            // Arrange
            var db = new InMemoryDatabase();
            var recipeManager = new RecipeManager(db);
            // Act & Assert
            Assert.Throws<ArgumentException>(() => recipeManager.AddRecipe(""));
        }

        [Fact]
        public void AddIngredientSucceeds()
        {
            // Arrange
            var db = new InMemoryDatabase();
            var recipeManager = new RecipeManager(db);
            var recipe = recipeManager.AddRecipe("Test Recipe");
            // Act
            var ok = recipeManager.AddIngredient(recipe.Id, "Test Ingredient");
            // Assert
            Assert.True(ok);
            var fetched = recipeManager.GetRecipeById(recipe.Id);
            Assert.Contains("Test Ingredient", fetched!.Ingredients);

        }
    }
}
