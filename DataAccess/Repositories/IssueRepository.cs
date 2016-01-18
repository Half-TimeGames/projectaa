using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataAccess.Interfaces;
using Entities;

namespace DataAccess.Repositories
{
    public class IssueRepository : IIssueRepository
    {
        private readonly IDbConnection _dbConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString);
        //private readonly IDbConnection _dbConnection = new SqlConnection("Data Source=LENOVO-PC\\SQLEXPRESS;Initial Catalog=Projectaa_Db;Integrated Security=True");

        public Issue Add(Issue issue)
        {
            var sqlQuery = "INSERT INTO Issue (Description) " +
                           "VALUES (@Description)";
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
