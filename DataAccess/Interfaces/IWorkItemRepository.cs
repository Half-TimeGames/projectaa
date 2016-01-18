using Entities;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IWorkItemRepository
    {
        List<WorkItem> GetAll();
        WorkItem Find(int id);
        WorkItem Add(WorkItem workItem);
        WorkItem Update(WorkItem workItem);
        void Remove(int id);
        List<WorkItem> FindByDescription(string text);
        List<WorkItem> FindIfIssue();
        List<WorkItem> FindByStatus(int statusId);
    }
}
