using DataAccess.Interfaces;
using Entities;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccess.Repositories
{
    public class IssueRepository : IIssueRepository
    {
        private readonly SqlConnection _dbConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString);

        public Issue Add(Issue issue)
        {
            const string sqlQuery = "INSERT INTO Issue (Description) " +
                                    "VALUES (@Description)" +
                                    "SELECT Id FROM Issue WHERE Id = scope_identity()";
            var issueId = _dbConnection.QueryWithRetry<int>(sqlQuery, issue).Single();
            issue.Id = issueId;
            return issue;
        }

        public Issue Find(int id)
        {
            return _dbConnection.QueryWithRetry<Issue>("SELECT * FROM Issue " +
                                              "WHERE Id = @Id", new { id }).SingleOrDefault();
        }

        public List<Issue> GetAll()
        {
            return _dbConnection.QueryWithRetry<Issue>("SELECT * FROM Issue").ToList();
        }

        public List<Issue> GetAll(int page, int perPage)
        {
            var sqlQuery = "DECLARE @PageNumber AS INT, @RowspPage AS INT " +
                           "SET @PageNumber = " + page + " " +
                           "SET @RowspPage = " + perPage + " " +
                           "SELECT* FROM ( " +
                           "SELECT ROW_NUMBER() OVER(ORDER BY Id) AS NUMBER, " +
                           "Id, Description FROM Issue" +
                           ") AS TBL " +
                           "WHERE NUMBER  BETWEEN((@PageNumber - 1) * @RowspPage + 1)  AND(@PageNumber * @RowspPage)" +
                           "ORDER BY Id";
            return _dbConnection.QueryWithRetry<Issue>(sqlQuery).ToList();
        }

        public WorkItem GetWorkItem(int id)
        {
            return _dbConnection.QueryWithRetry<WorkItem>("SELECT * FROM WorkItems " +
                                                 "WHERE Issue_Id = @Id", new { id }).Single();
        }

        public void Remove(int id)
        {
            const string sqlQuery = "DELETE FROM Issue " +
                                    "WHERE Id = @Id";
            _dbConnection.QueryWithRetry<Issue>(sqlQuery, new { id });
        }

        public Issue Update(Issue issue)
        {
            const string sqlQuery = "UPDATE Issue SET " +
                                    "Description = @Description " +
                                    "WHERE Id = @Id " +
                                    "SELECT * FROM Issue WHERE Id = @Id";
            var newIssue = _dbConnection.QueryWithRetry<Issue>(sqlQuery, issue).Single();
            return newIssue;
        }

    }
}
