using System;
using Entities;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IWorkItemRepository
    {
        List<WorkItem> GetAll(int page, int perPage);
        WorkItem Find(int id);
        WorkItem Add(WorkItem workItem);
        WorkItem Update(WorkItem workItem);
        WorkItem UpdateStatus(int statusId, int workItemId);
        void Remove(int id);
        List<WorkItem> FindByDescription(string text);
        List<WorkItem> FindIfIssue();
        List<WorkItem> FindByStatus(int statusId);
        List<WorkItem> FindByDate(DateTime from, DateTime to);
    }
}
