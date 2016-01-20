using System;
using DataAccess.Interfaces;
using Entities;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccess.Repositories
{
    public class WorkItemRepository : IWorkItemRepository
    {
        private readonly SqlConnection _dbConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString);

        public List<WorkItem> GetAll()
        {
            return _dbConnection.QueryWithRetry<WorkItem>("SELECT * FROM WorkItem").ToList();
        }

        public List<WorkItem> GetAll(int page, int perPage)
        {
            var sqlQuery = "DECLARE @PageNumber AS INT, @RowspPage AS INT " +
                           "SET @PageNumber = " + page + " " +
                           "SET @RowspPage = " + perPage + " " +
                           "SELECT* FROM ( " +
                           "SELECT ROW_NUMBER() OVER(ORDER BY Id) AS NUMBER, " +
                           "Id, Title, Description, DateCreated, DateFinished, Status_Id FROM WorkItem" +
                           ") AS TBL " +
                           "WHERE NUMBER  BETWEEN((@PageNumber - 1) * @RowspPage + 1)  AND(@PageNumber * @RowspPage)" +
                           "ORDER BY Id";
            return _dbConnection.QueryWithRetry<WorkItem>(sqlQuery).ToList();
        }

        public WorkItem Find(int id)
        {
            return _dbConnection.QueryWithRetry<WorkItem>("SELECT * FROM WorkItem " +
                                                "WHERE Id = @Id", new { id }).Single();
        }

        public WorkItem Add(WorkItem workItem)
        {
            var sqlQuery = "INSERT INTO WorkItem (Title, Description, DateCreated, Status_Id, DateFinished) " +
                           "VALUES (@Title, @Description,'" + DateTime.Now.ToShortDateString() + "', 1, NULL)" +
                           "SELECT Id FROM WorkItem WHERE Id = scope_identity()";
            if (workItem == null) return null;
            var workitemId = _dbConnection.QueryWithRetry<WorkItem>(sqlQuery, new { workItem.Title, workItem.Description }).First();
            workItem.Id = workitemId.Id;
            workItem.DateCreated = DateTime.Now.ToShortDateString();
            return workItem;
        }

        public void Remove(int id)
        {
            const string sqlQuery = "DELETE FROM WorkItem " +
                                    "WHERE Id = @Id";
            _dbConnection.QueryWithRetry<WorkItem>(sqlQuery, new { id });
        }

        public WorkItem AddUserToWorkItem(int userId, int workItemId)
        {
            const string sqlQuery = "UPDATE WorkItem " +
                                 "SET " +
                                 "User_Id = @UserId " +
                                 "WHERE Id = @WorkItemId " +
                                 "SELECT * FROM WorkItem WHERE Id = @WorkItemId";
            return _dbConnection.QueryWithRetry<WorkItem>(sqlQuery, new {UserId = userId, WorkItemId = workItemId}).Single();
        }

        public WorkItem AddIssue(int issueId, int workItemId)
        {
            const string sqlQuery = "UPDATE WorkItem " +
                                    "SET " +
                                    "Issue_id = @IssueId " +
                                    "WHERE Id = @WorkItemId " +
                                    "SELECT * FROM WorkItem WHERE Id = @WorkItemId";
            return _dbConnection.QueryWithRetry<WorkItem>(sqlQuery, new {IssueId = issueId, WorkItemId = workItemId}).Single();
        }

        public List<WorkItem> FindByDescription(string text)
        {
            return
                _dbConnection.QueryWithRetry<WorkItem>(
                    "SELECT * FROM WorkItem " +
                    "WHERE Description LIKE '%" + text + "%'")
                    .ToList();
        }

        public List<WorkItem> FindIfIssue()
        {
            return
                _dbConnection.QueryWithRetry<WorkItem>(
                    "SELECT * FROM WorkItem " +
                    "WHERE Issue_Id IS NOT NULL")
                    .ToList();
        }

        public List<WorkItem> FindByStatus(int statusId)
        {
            var sqlQuery = "SELECT * FROM WorkItem " +
                           "WHERE Status_Id = @StatusId";
            return _dbConnection.QueryWithRetry<WorkItem>(sqlQuery, new { StatusId = statusId }).ToList();
        }

        public List<WorkItem> FindByDate(DateTime from, DateTime to)
        {
            var sqlQuery = "DECLARE @From DATE, @To DATE " +
                           "SET @From = '" + from + "'" +
                           "SET @To = '" + to + "'" +
                           "SELECT* FROM WorkItem " +
                           "WHERE Status_Id = 3 AND (DateFinished BETWEEN @From AND @To)";
            return _dbConnection.QueryWithRetry<WorkItem>(sqlQuery).ToList();
        }



        public WorkItem UpdateStatus(int statusId, int workItemId)
        {
            DateTime? time;

            if (statusId == 3)
            {
                time = DateTime.Now;
            }
            else
            {
                time = null;
            }
            const string sqlQuery = "UPDATE WorkItem " +
                                   "SET " +
                                   "Status_Id = @statusId," +
                                   "DateFinished = @time" +
                                   " WHERE Id = @workItemId;" +
                                   " SELECT * FROM WorkItem " +
                                   " WHERE Id = @workItemId";
            return _dbConnection.QueryWithRetry<WorkItem>(sqlQuery, new { statusId, time, workItemId }).First();
        }

        public WorkItem FindByIssue(int issueId)
        {
            const string sqlQuery = "SELECT * FROM WorkItem " +
                                    "WHERE Issue_Id = @IssueId";
            return _dbConnection.QueryWithRetry<WorkItem>(sqlQuery, new { IssueId = issueId }).Single();
        }
    }
}
