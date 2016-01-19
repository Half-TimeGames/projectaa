using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DataAccess.Interfaces;
using Entities;

namespace DataAccess.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly IDbConnection _dbConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString);

        public Team Add(Team team)
        {
            const string sqlQuery = "INSERT INTO Team (Name) " +
                                    "VALUES (@Name)";
            var teamId = _dbConnection.Query(sqlQuery, team).Single();
            team.Id = teamId;
            return team;
        }

        public Team Find(int id)
        {
            return _dbConnection.Query<Team>("SELECT * FROM Team " +
                                             "WHERE Id = @TeamId", new { Teamid = id }).Single();
        }

        public List<Team> GetAll()
        {
            return _dbConnection.Query<Team>("SELECT * FROM Team").ToList();
        }

        public List<User> GetUsers(int id)
        {
            const string sqlQuery = "SELECT User_Id FROM TeamUser " +
                                    "WHERE Team_Id = @TeamId";
            var userIdList = _dbConnection.Query<int>(sqlQuery, new { TeamId = id }).ToList();

            return userIdList.Select(i => _dbConnection.Query<User>("SELECT * FROM [User] " +
                                                                    "WHERE Id = @UserId", new { UserId = i }).Single()).ToList();
        }

        public List<WorkItem> GetWorkItems(int id)
        {
            return _dbConnection.Query<WorkItem>("SELECT * FROM WorkItem " +
                                                 "WHERE Team_Id = @TeamId", new { TeamId = id }).ToList();
        }

        public void Remove(int id)
        {
            _dbConnection.Query("DELETE * FROM Team " +
                                "WHERE Id = @TeamId", new { TeamId = id });
        }

        public Team Update(Team team)
        {
            const string sqlQuery = "UPDATE Team " +
                                    "SET Name = @Name " +
                                    "WHERE Id = @Id";
            _dbConnection.Execute(sqlQuery);
            return team;
        }

        public Team AddUserToTeam(int userId, int teamId)
        {
            const string sqlQuery = "INSERT INTO TeamUser (Team_Id, User_Id) " +
                                    "VALUES (@Team_Id, @User_Id) " +
                                    "SELECT * FROM Team " +
                                    "WHERE Id = @Team_Id";
            return _dbConnection.Query<Team>(sqlQuery, new { User_Id = userId, Team_Id = teamId }).First();
        }
    }
}
