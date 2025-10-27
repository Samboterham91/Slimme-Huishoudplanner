using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimmeHuishoudplanner.Domain;
using DomainTask = SlimmeHuishoudplanner.Domain.Task; // Om verwarring met System.Threading.Tasks.Task te voorkomen!

namespace SlimmeHuishoudplanner.Persistence
{
    public class InMemoryDatabase : IDatabase
    {
        private readonly List<User> users = new();
        private readonly List<DomainTask> tasks = new();
        private readonly List<Recipe> recipes = new();
        private readonly List<InventoryItem> inventoryItems = new();
        private readonly List<ShoppingItem> shoppingItems = new();

        private int userId = 1;
        private int taskId = 1;
        private int recipeId = 1;
        private int inventoryItemId = 1;
        private int shoppingItemId = 1;

        // Users
        public User AddUser(User user)
        {
            user.Id = userId++;
            users.Add(user);
            return user;
        }
        public IEnumerable<User> GetUsers()
        {
            return users;
        }
        public User? GetUserById(int id)
        {
            return users.FirstOrDefault(u => u.Id == id);
        }
        public bool DeleteUser(int id)
        {
            var user = GetUserById(id);
            if (user == null) return false;
            return users.Remove(user);
        }

        // Tasks
        public DomainTask AddTask(DomainTask task)
        {
            task.Id = taskId++;
            tasks.Add(task);
            return task;
        }
        public IEnumerable<DomainTask> GetTasks()
        {
            return tasks;
        }
        public DomainTask? GetTaskById(int id)
        {
            return tasks.FirstOrDefault(t => t.Id == id);
        }
        public bool UpdateTask(DomainTask task)
        {
            var existingTask = GetTaskById(task.Id);
            if (existingTask == null) return false;
            existingTask.Description = task.Description;
            existingTask.AssignedToUserId = task.AssignedToUserId;
            return true;
        }
        public bool DeleteTask(int id)
        {
            var task = GetTaskById(id);
            if (task == null) return false;
            return tasks.Remove(task);
        }

        // Recipes
        public Recipe AddRecipe(Recipe recipe)
        {
            recipe.Id = recipeId++;
            recipes.Add(recipe);
            return recipe;
        }
        public IEnumerable<Recipe> GetRecipes()
        {
            return recipes;
        }
        public Recipe? GetRecipeById(int id)
        {
            return recipes.FirstOrDefault(r => r.Id == id);
        }
        public bool DeleteRecipe(int id)
        {
            var recipe = GetRecipeById(id);
            if (recipe == null) return false;
            return recipes.Remove(recipe);
        }
        // Inventory
        public InventoryItem AddInventoryItem(InventoryItem item)
        {
            item.Id = inventoryItemId++;
            inventoryItems.Add(item);
            return item;
        }
        public IEnumerable<InventoryItem> GetInventoryItems()
        {
            return inventoryItems;
        }
        public InventoryItem? GetInventoryItemById(int id)
        {
            return inventoryItems.FirstOrDefault(i => i.Id == id);
        }
        public bool UpdateInventoryItem(InventoryItem item)
        {
            var existingItem = GetInventoryItemById(item.Id);
            if (existingItem == null) return false;
            existingItem.Name = item.Name;
            existingItem.Quantity = item.Quantity;
            return true;
        }
        public bool DeleteInventoryItem(int id)
        {
            var item = GetInventoryItemById(id);
            if (item == null) return false;
            return inventoryItems.Remove(item);
        }
        // Shopping
        public ShoppingItem AddShoppingItem(ShoppingItem item)
        {
            item.Id = shoppingItemId++;
            shoppingItems.Add(item);
            return item;
        }
        public IEnumerable<ShoppingItem> GetShoppingList()
        {
            return shoppingItems;
        }
        public ShoppingItem? GetShoppingItemById(int id)
        {
            return shoppingItems.FirstOrDefault(s => s.Id == id);
        }
        public bool UpdateShoppingItem(ShoppingItem item)
        {
            var existingItem = GetShoppingItemById(item.Id);
            if (existingItem == null) return false;
            existingItem.Name = item.Name;
            existingItem.Quantity = item.Quantity;
            if (item.IsCheckedOff)
            {
                existingItem.CheckOff();
            }
            else
            {
                existingItem.Uncheck();
            }
            return true;
        }
        public bool DeleteShoppingItem(int id)
        {
            var item = GetShoppingItemById(id);
            if (item == null) return false;
            return shoppingItems.Remove(item);
        }
    }
}
