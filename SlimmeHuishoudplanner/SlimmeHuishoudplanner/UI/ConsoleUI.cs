using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimmeHuishoudplanner.Application;
using SlimmeHuishoudplanner.Persistence;

namespace SlimmeHuishoudplanner.UI
{
    public class ConsoleUI
    {
        private readonly TaskManager _taskManager;
        private readonly RecipeManager _recipeManager;
        public ConsoleUI()
        {
            var db = new InMemoryDatabase();
            _taskManager = new TaskManager(db);
            _recipeManager = new RecipeManager(db);
        }

        public void Run()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n --- Slimme Huishoudplanner --- ");
                Console.ResetColor();
                Console.WriteLine("1. Taken beheren");
                Console.WriteLine("2. Recepten beheren");
                Console.WriteLine("0. Afsluiten");
                Console.Write("Maak een keuze: ");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ShowTasksMenu();
                        break;
                    case "2":
                        ShowRecipesMenu();
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Ongeldige keuze, probeer het opnieuw.");
                        break;
                }
            }
        }

        private void ShowTasksMenu()
        {
            bool inTasksMenu = true;
            while (inTasksMenu)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n --- Taken Beheren --- ");
                Console.ResetColor();
                Console.WriteLine("1. Taak toevoegen");
                Console.WriteLine("2. Alle taken weergeven");
                Console.WriteLine("3. Taak afvinken");
                Console.WriteLine("0. Terug naar hoofdmenu");
                Console.Write("Maak een keuze: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.Write("Voer taakbeschrijving in: ");
                        var description = Console.ReadLine() ?? "";
                        try
                        {
                           var task = _taskManager.AddTask(description);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Taak '{task.Description}' succesvol toegevoegd!");
                            Console.ResetColor();
                        }
                        catch (ArgumentException ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Fout bij toevoegen taak: {ex.Message}");
                            Console.ResetColor();
                        }
                        break;
                    case "2":
                        Console.WriteLine("\n --- Takenlijst --- ");
                        foreach (var task in _taskManager.GetTasks())
                        {
                            Console.WriteLine($"{task.Id}. {(task.IsDone ? "[X]" : "[ ]")} {task.Description}");
                        }
                        Console.WriteLine();
                        break;
                    case "3":
                        Console.Write("Voer het ID van de taak in om af te vinken: ");
                        if (int.TryParse(Console.ReadLine(), out int taskId))
                        {
                            if (_taskManager.MarkAsDone(taskId))
                                Console.WriteLine("Taak succesvol afgevinkt!");
                            else
                                Console.WriteLine("Taak niet gevonden.");
                        }
                        else Console.WriteLine("Ongeldig ID ingevoerd.");
                        break;
                    case "0":
                        inTasksMenu = false;
                        break;
                    default:
                        Console.WriteLine("Ongeldige keuze, probeer het opnieuw.");
                        break;
                }
                if (inTasksMenu) Pause();
            }
        }

        private void ShowRecipesMenu()
        {
            bool inRecipesMenu = true;
            while (inRecipesMenu)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n --- Recepten Beheren --- ");
                Console.ResetColor();
                Console.WriteLine("1. Recept toevoegen");
                Console.WriteLine("2. Alle recepten weergeven");
                Console.WriteLine("3. Ingrediënt toevoegen aan recept");
                Console.WriteLine("4. Ingredient verwijderen van recept");
                Console.WriteLine("0. Terug naar hoofdmenu");
                Console.Write("Maak een keuze: ");
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Console.Write("Voer receptnaam in: ");
                        var name = Console.ReadLine() ?? "";
                        try
                        {
                            var recipe = _recipeManager.AddRecipe(name);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Recept '{recipe.Name}' succesvol toegevoegd!");
                            Console.ResetColor();
                        }
                        catch (ArgumentException ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Fout bij toevoegen recept: {ex.Message}");
                            Console.ResetColor();
                        }
                        break;
                    case "2":
                        Console.WriteLine("\n --- Receptenlijst --- ");
                        foreach (var recipe in _recipeManager.GetRecipes())
                        {
                            Console.WriteLine($"{recipe.Id}: {recipe.Name}");
                            if (recipe.Ingredients.Count > 0)
                                Console.WriteLine($"  Ingrediënten: {string.Join(", ", recipe.Ingredients)}");
                            else
                                Console.WriteLine("  Geen ingrediënten toegevoegd.");
                        }
                        break;
                    case "3":
                        Console.Write("Voer het ID van het recept in: ");
                        if (int.TryParse(Console.ReadLine(), out int recipeId))
                        {
                            Console.Write("Voer ingrediënt in om toe te voegen: ");
                            var ingredientToAdd = Console.ReadLine() ?? "";
                            try
                            {
                                if (_recipeManager.AddIngredient(recipeId, ingredientToAdd))
                                    Console.WriteLine("Ingrediënt succesvol toegevoegd!");
                                else
                                    Console.WriteLine("Recept niet gevonden.");
                            }
                            catch (ArgumentException ex)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Fout bij toevoegen ingrediënt: {ex.Message}");
                                Console.ResetColor();
                            }
                        }
                        else Console.WriteLine("Ongeldige invoer.");
                        break;
                    case "4":
                        Console.WriteLine("Voer het ID van het recept in: ");
                        if (int.TryParse(Console.ReadLine(), out int recId))
                        {
                            Console.Write("Voer ingrediënt in om te verwijderen: ");
                            var ingredientToRemove = Console.ReadLine() ?? "";
                            try
                            {
                                if (_recipeManager.RemoveIngredient(recId, ingredientToRemove))
                                    Console.WriteLine("Ingrediënt succesvol verwijderd!");
                                else
                                    Console.WriteLine("Recept niet gevonden of ingrediënt bestaat niet.");
                            }
                            catch (ArgumentException ex)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Fout bij verwijderen ingrediënt: {ex.Message}");
                                Console.ResetColor();
                            }
                        }
                        else Console.WriteLine("Ongeldige invoer.");
                        break;
                    case "0":
                        inRecipesMenu = false;
                        break;
                    default:
                        Console.WriteLine("Ongeldige keuze, probeer het opnieuw.");
                        break;
                }
                if (inRecipesMenu) Pause();
            }
        }

        private void Pause()
        {
            Console.WriteLine("\n Druk op een toets om door te gaan...");
            Console.ReadKey();
        }

    }
}
