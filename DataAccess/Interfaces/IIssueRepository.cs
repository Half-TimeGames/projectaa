using System.Collections.Generic;
using Entities;

namespace DataAccess.Interfaces
{
    public interface IIssueRepository
    {
        List<Issue> GetAll();
        Issue Find(int id);
        Issue Add(Issue issue);
        Issue Update(Issue issue);
        void Remove(int id);
        WorkItem GetWorkItem(int id);
    }
}
