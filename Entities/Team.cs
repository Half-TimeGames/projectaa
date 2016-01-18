using System.Collections.Generic;

namespace Entities
{
    public sealed class Team
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<WorkItem> WorkItems { get; set; }  
    }
}
