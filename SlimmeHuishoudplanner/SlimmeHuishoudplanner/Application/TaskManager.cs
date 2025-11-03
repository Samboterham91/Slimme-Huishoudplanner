using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimmeHuishoudplanner.Domain;
using SlimmeHuishoudplanner.Persistence;
using DomainTask = SlimmeHuishoudplanner.Domain.Task; // Om verwarring met System.Threading.Tasks.Task te voorkomen!

namespace SlimmeHuishoudplanner.Application
{
    public class TaskManager
    {
        private readonly IDatabase _db;
        public TaskManager(IDatabase database)
        {
            _db = database;
        }
        // Task aanmaken
        public DomainTask AddTask(string description, int? assignedToUserId = null)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Beschrijving mag niet leeg zijn.");
            }

            if (assignedToUserId.HasValue && _db.GetUserById(assignedToUserId.Value) == null)
            {
                throw new ArgumentException("Toegewezen gebruiker bestaat niet.");
            }

                var task = new DomainTask
            {
                Description = description,
                AssignedToUserId = assignedToUserId
            };
            return _db.AddTask(task);
        }
        // Tasks opvragen
        public IEnumerable<DomainTask> GetTasks()
        {
            return _db.GetTasks();
        }
        public DomainTask? GetTaskById(int id)
        {
            return _db.GetTaskById(id);
        }
        // Task bijwerken
        public bool UpdateTask(DomainTask task)
        {
            return _db.UpdateTask(task);
        }
        // Task toewijzen aan gebruiker
        public bool AssignTaskToUser(int taskId, int userId)
        {
            var task = _db.GetTaskById(taskId);
            if (task == null) return false;
            task.AssignedToUserId = userId;
            return _db.UpdateTask(task);
        }
        // Task als voltooid markeren
        public bool MarkAsDone(int taskId)
        {
            var task = _db.GetTaskById(taskId);
            if (task == null) return false;
            task.MarkAsDone();
            return _db.UpdateTask(task);
        }
        // Task verwijderen
        public bool DeleteTask(int id)
        {
            return _db.DeleteTask(id);
        }
    }
}
