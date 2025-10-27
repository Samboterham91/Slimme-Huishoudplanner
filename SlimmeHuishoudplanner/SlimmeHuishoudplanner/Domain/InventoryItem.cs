using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimmeHuishoudplanner.Domain
{
    public class InventoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string Unit { get; set; } = "stuks";

        public void IncreaseQuantity(int amount)
        {
            Quantity += amount;
        }

        public void DecreaseQuantity(int amount)
        {
            Quantity -= amount;
            if (Quantity < 0)
            {
                Quantity = 0;
            }
        }
    }
}
