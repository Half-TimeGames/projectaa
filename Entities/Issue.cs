using System.Collections.Generic;

namespace Entities
{
    public sealed class Issue
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public ICollection<WorkItem> WorkItems { get; set; } 
    }
}
