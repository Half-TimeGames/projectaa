namespace Entities
{
    public sealed class WorkItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DateCreated { get; set; }
        public string DateFinished { get; set; }
        public int Status_Id { get; set; }
        public int? User_Id { get; set; }
        public int? Issue_Id { get; set; }
        public int? Team_Id { get; set; }
    }
}
