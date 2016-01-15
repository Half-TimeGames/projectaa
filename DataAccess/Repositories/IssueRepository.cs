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
        private readonly IDbConnection _dbConnection = new SqlConnection("Data Source=MAJOR\\S" +
                                                                "QLEXPRESS;Initial Catalog=Projectaa_Db;Integrated Security=True");
        public Issue Add(Issue issue)
        {
            const string sqlQuery = "insert into Issue (Description) values (@Description)";
            var issueId = _dbConnection.Query<int>(sqlQuery, issue).Single();
            issue.Id = issueId;
            return issue;
        }

        public Issue Find(int id)
        {
            return _dbConnection.Query<Issue>("select * from Issue where Id = @Id", id).SingleOrDefault();

        }

        public List<Issue> GetAll()
        {
            return _dbConnection.Query<Issue>("select * from Issue").ToList();
        }

        public void Remove(int id)
        {
            const string sqlQuery = "delete from Issue where Id = @Id";
            _dbConnection.Execute(sqlQuery, id);
        }

        public Issue Update(Issue issue)
        {
            const string sqlQuery = "update Issue set Description = @Description where Id = @Id";
            _dbConnection.Execute(sqlQuery, issue);
            return issue;
        }
    }
}
