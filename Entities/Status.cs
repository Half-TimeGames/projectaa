using System.Collections.Generic;

namespace Entities
{
    public sealed class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<WorkItem> WorkItems { get; set; } 
    }
}
