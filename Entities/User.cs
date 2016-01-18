using System.Collections.Generic;

namespace Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        public ICollection<Team> Teams { get; set; }
        public ICollection<WorkItem> WorkItems { get; set; } 
    }
}
