using Dapper;
using DataAccess.Interfaces;
using Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccess.Repositories
{
    public class WorkItemRepository : IWorkItemRepository
    {
        private readonly IDbConnection _dbConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString);

        public List<WorkItem> GetAll(int pageNumber, int rowsPerPage)
        {
            var sqlQuery = "DECLARE @PageNumber AS INT, @RowspPage AS INT " +
                           "SET @PageNumber = " + pageNumber + " " +
                           "SET @RowspPage = " + rowsPerPage + " " +
                           "SELECT* FROM ( " +
                           "SELECT ROW_NUMBER() OVER(ORDER BY Id) AS NUMBER, " +
                           "Id, Title, Description, DateCreated, DateFinished, Status_Id FROM WorkItem" +
                           ") AS TBL " +
                           "WHERE NUMBER  BETWEEN((@PageNumber - 1) * @RowspPage + 1)  AND(@PageNumber * @RowspPage)" +
                           "ORDER BY Id";
            return _dbConnection.Query<WorkItem>(sqlQuery).ToList();
        }

        public WorkItem Find(int id)
        {
            return _dbConnection.Query<WorkItem>("SELECT * FROM WorkItem " +
                                                "WHERE Id = @Id", new { id }).Single();
        }

        public WorkItem Add(WorkItem workItem)
        {
            var sqlQuery = "INSERT INTO WorkItem (Title, Description, DateCreated, Status_Id, DateFinished) " +
                           "VALUES (@Title, @Description,'" + DateTime.Now.ToShortDateString() + "', 1, NULL)" +
                           "SELECT Id FROM WorkItem WHERE Id = scope_identity()";
            if (workItem == null) return null;
            var workitemId = _dbConnection.Query(sqlQuery, new {workItem.Title, workItem.Description}).First();
            workItem.Id = workitemId.Id;
            workItem.DateCreated = DateTime.Now.ToShortDateString();
            return workItem;
        }

        public WorkItem Update(WorkItem workItem)
        {
            var sqlQuery = "UPDATE WorkItem " +
                           "SET " +
                           "Title = @Title," +
                           "Description = @Description," +
                           "Status_Id = @StatusId," +
                           "Issue_Id = @IssueId," +
                           "Team_Id = @TeamId," +
                           "User_Id = @UserId" +
                           " WHERE Id = @Id";
            _dbConnection.Execute(sqlQuery, workItem);
            return workItem;
        }

        public void Remove(int id)
        {
            var sqlQuery = "DELETE FROM WorkItem " +
                           "WHERE Id = @Id";
            _dbConnection.Execute(sqlQuery, new { id });
        }

        public WorkItem AddUserToWorkItem(int userId, int workItemId)
        {
            const string sqlQuery = "UPDATE WorkItem " +
                                 "SET " +
                                 "User_Id = @UserId " +
                                 "WHERE Id = @WorkItemId " +
                                 "SELECT * FROM WorkItem WHERE Id = @WorkItemId";
            return _dbConnection.Query<WorkItem>(sqlQuery, new {UserId = userId, WorkItemId = workItemId}).Single();
        }

        public WorkItem ChangeStatus(int statusId, int workItemId)
        {
            const string sqlQuery = "UPDATE WorkItem " +
                                    "SET " +
                                    "Status_id = @StatusId " +
                                    "WHERE Id = @WorkItemId " +
                                    "SELECT * FROM WorkItem WHERE Id = @WorkItemId";
            return _dbConnection.Query<WorkItem>(sqlQuery, new {StatusId = statusId, WorkItemId = workItemId}).Single();
        }

        public List<WorkItem> FindByDescription(string text)
        {
            return
                _dbConnection.Query<WorkItem>(
                    "SELECT * FROM WorkItem " +
                    "WHERE Description LIKE '%" + text + "%'")
                    .ToList();
        }

        public List<WorkItem> FindIfIssue()
        {
            return
                _dbConnection.Query<WorkItem>(
                    "SELECT * FROM WorkItem " +
                    "WHERE Issue_Id IS NULL")
                    .ToList();
        }

        public List<WorkItem> FindByStatus(int statusId)
        {
            var sqlQuery = "SELECT * FROM WorkItem " +
                           "WHERE Status_Id = @StatusId";
            return _dbConnection.Query<WorkItem>(sqlQuery, new { StatusId = statusId }).ToList();
        }
    }
}
