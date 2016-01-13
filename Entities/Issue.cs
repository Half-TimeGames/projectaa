using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Issue
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public ICollection<WorkItem> WorkItems { get; set; } 
    }
}
