using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SlimmeHuishoudplanner.Persistence;
using SlimmeHuishoudplanner.Application;
using SlimmeHuishoudplanner.Domain;
using DomainTask = SlimmeHuishoudplanner.Domain.Task; // Om verwarring met System.Threading.Tasks.Task te voorkomen!

namespace SlimmeHuishoudplanner.Tests.Application
{
    public class ShoppingManagerTests
    {
        [Fact]
        public void CheckOff_SetsItemCheckedTrue()
        {
            // Arrange
            var db = new InMemoryDatabase();
            var shopping = new ShoppingManager(db);

            var item = shopping.AddItem("Brood", 1, "stuks");

            // Act
            var ok = shopping.CheckOff(item.Id);
            var updated = shopping.GetItemById(item.Id);

            // Assert
            Assert.True(ok);
            Assert.NotNull(updated);
            Assert.True(updated!.IsCheckedOff);
        }
    }
}
