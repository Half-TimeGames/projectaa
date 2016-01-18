using System.Collections.Generic;

namespace Entities
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<WorkItem> WorkItems { get; set; } 
    }
}
