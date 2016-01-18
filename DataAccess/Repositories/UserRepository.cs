using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DataAccess.Interfaces;
using Entities;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _dbConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString);
        //private readonly IDbConnection _dbConnection = new SqlConnection("Data Source=LENOVO-PC\\SQLEXPRESS;Initial Catalog=Projectaa_Db;Integrated Security=True");

        public User Add(User user)
        {
            const string sqlQuery = "INSERT INTO [User] (FirstName, LastName, UserName) " +
                                    "VALUES (@" +
                                    "FirstName, @LastName, @UserName)"+
                                    "SELECT Id FROM [User] WHERE Id = scope_identity()";
            if (user == null) return null;
            var userId = _dbConnection.Query(sqlQuery, new {user.FirstName, user.LastName, user.UserName}).First();
                
            user.Id = userId.Id;

            return user;
        }

        public User Find(int id)
        {
            return _dbConnection.Query<User>("SELECT * FROM [User] " +
                                             "WHERE Id = @Id", new { id }).SingleOrDefault();
        }

        public List<User> FindByName(string name)
        {
            return
                _dbConnection.Query<User>("Declare @Name varchar(100);" +
                                            "Set @Name = '%" + name + "%';" +
                                            "SET @Name = '%" + name + "%';" +
                                            "SELECT * FROM [User] " +
                                            "WHERE (FirstName LIKE @Name) or " +
                                            "(LastName LIKE @Name) or " +
                                            "(UserName LIKE @Name)").ToList();
        }

        public List<User> GetAll()
        {
            return _dbConnection.Query<User>("SELECT * FROM [User]").ToList();
        }

        public List<Team> GetTeams(int id)
        {
            const string sqlQuery = "SELECT Team_Id FROM TeamUser " +
                                    "WHERE User_Id = @UserId";
            var teamIdList = _dbConnection.Query<int>(sqlQuery, new { UserId = id }).ToList();

            return teamIdList.Select(i => _dbConnection.Query<Team>("SELECT * FROM Team " +
                                                                    "WHERE Id = @TeamId", new { TeamId = i }).Single()).ToList();
        }

        public void Remove(int id)
        {
            const string sqlQuery = "DELETE FROM [User] " +
                                    "WHERE Id = @Id";
            _dbConnection.Execute(sqlQuery, new { id });
        }

        public User Update(User user)
        {
            const string sqlQuery = "UPDATE [User] " +
                                    "SET " +
                                    "FirstName = @FirstName," +
                                    "LastName = @LastName," +
                                    "UserName = @UserName" +
                                    " WHERE Id = @Id";
            _dbConnection.Execute(sqlQuery, new {user.FirstName, user.LastName, user.UserName});
            return user;
        }

        public List<WorkItem> WorkItems(int id)
        {
            return _dbConnection.Query<WorkItem>("SELECT * FROM WorkItem " +
                                                 "WHERE User_Id = @UserId", new { UserId = id }).ToList();
        }
    }
}
