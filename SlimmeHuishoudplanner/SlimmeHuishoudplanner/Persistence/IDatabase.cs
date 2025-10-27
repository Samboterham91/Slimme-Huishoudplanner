using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimmeHuishoudplanner.Domain;
using DomainTask = SlimmeHuishoudplanner.Domain.Task; // Om verwarring met System.Threading.Tasks.Task te voorkomen! (ambigous foutmelding)

namespace SlimmeHuishoudplanner.Persistence
    
// Voor nu in-memory, later misschien SQLite
{
    public interface IDatabase
    {
       // Users
        User AddUser (User user);
        IEnumerable<User> GetUsers();
        User? GetUserById (int id);
        bool DeleteUser (int id);

        // Tasks (DomainTask om verwarring met System.Threading.Tasks.Task te voorkomen)
        DomainTask AddTask (DomainTask task);
        IEnumerable<DomainTask> GetTasks();
        DomainTask? GetTaskById (int id);
        bool UpdateTask (DomainTask task);
        bool DeleteTask (int id);

        // Recipes
        Recipe AddRecipe (Recipe recipe);
        IEnumerable<Recipe> GetRecipes();
        Recipe? GetRecipeById (int id);
        bool DeleteRecipe (int id);

        // Inventory
        InventoryItem AddInventoryItem (InventoryItem item);
        IEnumerable<InventoryItem> GetInventoryItems();
        InventoryItem? GetInventoryItemById (int id);
        bool UpdateInventoryItem (InventoryItem item);
        bool DeleteInventoryItem (int id);

        // Shopping
        ShoppingItem AddShoppingItem (ShoppingItem item);
        IEnumerable<ShoppingItem> GetShoppingList();
        ShoppingItem? GetShoppingItemById (int id);
        bool UpdateShoppingItem (ShoppingItem item);
        bool DeleteShoppingItem (int id);
    }
}
