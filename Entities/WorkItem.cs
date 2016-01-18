using System;

namespace Entities
{
    public sealed class WorkItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateDone { get; set; }
    }
}
