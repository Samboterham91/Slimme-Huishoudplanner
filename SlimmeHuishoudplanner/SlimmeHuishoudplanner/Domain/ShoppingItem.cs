using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimmeHuishoudplanner.Domain
{
    public class ShoppingItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string Unit { get; set; } = "stuks";
        public bool IsCheckedOff { get; private set; }

        public void CheckOff()
        {
            IsCheckedOff = true;
        }
        public void Uncheck()
        {
            IsCheckedOff = false;
        }
    }
}
