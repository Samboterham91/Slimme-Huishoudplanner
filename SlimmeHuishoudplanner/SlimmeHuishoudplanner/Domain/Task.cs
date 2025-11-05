using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimmeHuishoudplanner.Domain
{
    public class Task
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsDone { get; private set; }
        public int? AssignedToUserId { get; set; }

        public void MarkAsDone()
        {
            IsDone = true;
        }
    }
}
