using Dapper;
using DataAccess.Interfaces;
using Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccess.Repositories
{
    public class IssueRepository : IIssueRepository
    {
        private readonly IDbConnection _dbConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString);

        public Issue Add(Issue issue)
        {
            const string sqlQuery = "INSERT INTO Issue (Description) " +
                                    "VALUES (@Description)" +
                                    "SELECT Id FROM Issue WHERE Id = scope_identity()";
            var issueId = _dbConnection.Query<int>(sqlQuery, issue).Single();
            issue.Id = issueId;
            return issue;
        }

        public Issue Find(int id)
        {
            return _dbConnection.Query<Issue>("SELECT * FROM Issue " +
                                              "WHERE Id = @Id", new { id }).SingleOrDefault();
        }

        public List<Issue> GetAll()
        {
            return _dbConnection.Query<Issue>("SELECT * FROM Issue").ToList();
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
            return _dbConnection.Query<Issue>(sqlQuery).ToList();
        }

        public WorkItem GetWorkItem(int id)
        {
            return _dbConnection.Query<WorkItem>("SELECT * FROM WorkItems " +
                                                 "WHERE Issue_Id = @Id", new { id }).Single();
        }

        public void Remove(int id)
        {
            var sqlQuery = "DELETE FROM Issue " +
                           "WHERE Id = @Id";
            _dbConnection.Execute(sqlQuery, new { id });
        }

        public Issue Update(Issue issue)
        {
            var sqlQuery = "UPDATE Issue SET " +
                           "Description = @Description " +
                           "WHERE Id = @Id";
            _dbConnection.Execute(sqlQuery, issue);
            return issue;
        }

    }
}
