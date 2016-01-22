﻿namespace Entities
{
    public sealed class Issue
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public WorkItem WorkItem { get; set; } 
    }
}
