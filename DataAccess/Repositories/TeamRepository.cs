﻿using System;
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
        //private readonly IDbConnection _dbConnection = new SqlConnection("Server=tcp:projectaa.database.windows.net,1433;Database=projactaa_db;User ID=andreas.dellrud@projectaa;Password=TeAnAn2016;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        private readonly IDbConnection _dbConnection = new SqlConnection("Data Source=LENOVO-PC\\SQLEXPRESS;Initial Catalog=Projectaa_Db;Integrated Security=True");

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
    }
}