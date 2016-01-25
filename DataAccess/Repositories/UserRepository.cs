using DataAccess.Interfaces;
using Entities;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlConnection _dbConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString);

        public User Add(User user)
        {
            const string sqlQuery = "INSERT INTO [User] (FirstName, LastName, UserName) " +
                           "VALUES (@" +
                           "FirstName, @LastName, @UserName)"+
                           "SELECT Id FROM [User] WHERE Id = scope_identity()";
            if (user == null) return null;
            var userId = _dbConnection.QueryWithRetry<User>(sqlQuery, new {user.FirstName, user.LastName, user.UserName}).First();
                
            user.Id = userId.Id;

            return user;
        }

        public User Find(int id)
        {
            return _dbConnection.QueryWithRetry<User>("SELECT * FROM [User] " +
                                             "WHERE Id = @Id", new { id }).SingleOrDefault();
        }

        public List<User> FindByName(string firstName = null, string lastName = null, string userName = null)
        {
            if (firstName != null)
            {
                return _dbConnection.QueryWithRetry<User>("Declare @Name varchar(100);" +
                                "SET @Name = '%" + firstName + "%';" +
                                "SELECT * FROM [User] " +
                                "WHERE (FirstName LIKE @Name)").ToList();
            }
            if (lastName != null)
            {
                return _dbConnection.QueryWithRetry<User>("Declare @Name varchar(100);" +
                                "SET @Name = '%" + lastName + "%';" +
                                "SELECT * FROM [User] " +
                                "WHERE (LastName LIKE @Name)").ToList();
            }

            return _dbConnection.QueryWithRetry<User>("Declare @Name varchar(100);" +
                                "SET @Name = '%" + userName + "%';" +
                                "SELECT * FROM [User] " +
                                "WHERE (UserName LIKE @Name)").ToList();
        }

        public List<User> GetAll()
        {
            return _dbConnection.QueryWithRetry<User>("SELECT * FROM [User]").ToList();
        } 

        public List<User> GetAll(int page, int perPage)
        {
            var sqlQuery = "DECLARE @PageNumber AS INT, @RowspPage AS INT " +
                           "SET @PageNumber = " + page + " " +
                           "SET @RowspPage = " + perPage + " " +
                           "SELECT* FROM ( " +
                           "SELECT ROW_NUMBER() OVER(ORDER BY Id) AS NUMBER, " +
                           "Id, FirstName, LastName, UserName FROM [User]" +
                           ") AS TBL " +
                           "WHERE NUMBER  BETWEEN((@PageNumber - 1) * @RowspPage + 1)  AND(@PageNumber * @RowspPage)" +
                           "ORDER BY Id";

            return _dbConnection.QueryWithRetry<User>(sqlQuery).ToList();
        }

        public List<Team> GetTeams(int id)
        {
            const string sqlQuery = "SELECT Team_Id FROM TeamUser " +
                           "WHERE User_Id = @UserId";
            var teamIdList = _dbConnection.QueryWithRetry<int>(sqlQuery, new { UserId = id }).ToList();

            return teamIdList.Select(i => _dbConnection.QueryWithRetry<Team>("SELECT * FROM Team " +
                                                                    "WHERE Id = @TeamId", new { TeamId = i }).Single()).ToList();
        }

        public void Remove(int id)
        {
            const string sqlQuery = "DELETE FROM [User] " +
                           "WHERE Id = @Id";
            _dbConnection.QueryWithRetry<User>(sqlQuery, new { id });
        }
        public User AddTeamToUser(int userId, int teamId)
        {
            const string sqlQuery = "INSERT INTO TeamUser (Team_Id, User_Id) " +
                                    "VALUES (@Team_Id, @User_Id) " +
                                    "SELECT * FROM [User] " +
                                    "WHERE Id = @User_Id";
            return _dbConnection.QueryWithRetry<User>(sqlQuery, new { User_Id = userId, Team_Id = teamId }).First();
        }

        public User Update(User user)
        {
            const string sqlQuery = "UPDATE [User] " +
                           "SET " +
                           "FirstName = @FirstName," +
                           "LastName = @LastName," +
                           "UserName = @UserName" +
                           " WHERE Id = @Id " +
                           "SELECT * FROM [User] WHERE Id = @Id";
            var newUser = _dbConnection.QueryWithRetry<User>(sqlQuery, new {user.FirstName, user.LastName, user.UserName, user.Id}).Single();
            return newUser;
        }

        public List<WorkItem> WorkItems(int id)
        {
            return _dbConnection.QueryWithRetry<WorkItem>("SELECT * FROM WorkItem " +
                                                 "WHERE User_Id = @UserId", new { UserId = id }).ToList();
        }
    }
}
