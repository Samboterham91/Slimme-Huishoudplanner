using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SlimmeHuishoudplanner.Application;
using SlimmeHuishoudplanner.Persistence;
using SlimmeHuishoudplanner.Domain;
using DomainTask = SlimmeHuishoudplanner.Domain.Task; // Om verwarring met System.Threading.Tasks.Task te voorkomen!

namespace SlimmeHuishoudplanner.Tests.Application
{
    public class TaskManagerTests
    {
        [Fact]
        public void AddTaskSucceeds()
        {
            // Arrange
            var db = new InMemoryDatabase();
            var taskManager = new TaskManager(db);

            string description = "Test Task";
            // Act
            var task = taskManager.AddTask(description);
            // Assert
            Assert.NotNull(task);
            Assert.Equal(description, task.Description);
            Assert.False(task.IsDone);
            Assert.True(task.Id > 0);
        }

        [Fact]
        public void AddTaskFails_WhenDescriptionEmpty() // Poging om op een TDD-manier te programmeren vanuit deze in eerste instantie falende test --> exceptions toegevoegd in TaskManager.cs
        {
            // Arrange
            var db = new InMemoryDatabase();
            var taskManager = new TaskManager(db);

            string description = ""; // Ongeldige beschrijving
            // Act & Assert
            Assert.Throws<ArgumentException>(() => taskManager.AddTask(description));
        }

        [Fact]
        public void AddTaskFails_WhenUserDoesNotExist()
        {
            // Arrange
            var db = new InMemoryDatabase();
            var taskManager = new TaskManager(db);

            string description = "Test Task";
            int nonExistentUserId = 999; // Gebruiker bestaat niet in de database
            // Act & Assert
            Assert.Throws<ArgumentException>(() => taskManager.AddTask(description, nonExistentUserId));
        }

        [Fact]
        public void GetTaskById_ReturnsCorrectTask()
        {
            // Arrange
            var db = new InMemoryDatabase();
            var taskManager = new TaskManager(db);
            
            string description = "Test Task";
            // Act
            var addedTask = taskManager.AddTask(description);
            var fetchedTask = taskManager.GetTaskById(addedTask.Id);
            // Assert
            Assert.NotNull(fetchedTask); // Zorg dat de taak is opgehaald
            Assert.Equal(addedTask.Id, fetchedTask!.Id); // Vergelijk de IDs
            Assert.Equal(addedTask.Description, fetchedTask.Description); // Vergelijk de beschrijvingen
            Assert.False(fetchedTask.IsDone); // Controleer dat de taak niet als voltooid is gemarkeerd

            //Alle drie de lagen werken hier samen: (InMemoryDatabase, TaskManager, Task) dus er ontstaat een data-integriteitstest.
        }
    }
}
