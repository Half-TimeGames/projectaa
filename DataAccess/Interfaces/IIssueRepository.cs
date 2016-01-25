using Entities;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IIssueRepository
    {
        List<Issue> GetAll(int page, int perPage);
        Issue Find(int id);
        Issue Add(Issue issue);
        Issue Update(Issue issue);
        void Remove(int id);
        WorkItem GetWorkItem(int id);
    }
}
