using System;

namespace Entities
{
    public sealed class WorkItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DateCreated { get; set; }
        public string DateDone { get; set; }
    }
}
