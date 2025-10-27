using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimmeHuishoudplanner.Domain
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<string> Ingredients { get; set; } = new();
        public void AddIngredient(string name)
        {
            Ingredients.Add(name);
        }
        public void RemoveIngredient(string name)
        {
            Ingredients.Remove(name);
        }
    }
}
