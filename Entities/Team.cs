using System.Collections.Generic;

namespace Entities
{
    public class Team
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public ICollection<User> User { get; set; }
        public ICollection<WorkItem> WorkItems { get; set; }  
    }
}
