using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Team
    {
        public string Id { get; }
        public string Name { get; set; }

        public ICollection<User> User { get; set; }
        public ICollection<WorkItem> WorkItems { get; set; }  
    }
}
