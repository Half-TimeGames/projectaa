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
    public class WorkItemRepository : IWorkItemRepository
    {
        private readonly IDbConnection _dbConnection = new SqlConnection("Server=tcp:projectaa-server.database.windows.net,1433;Database=projectaa_db;User ID=projectaa@projectaa-server;Password=TeAnAn16;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        //private readonly IDbConnection _dbConnection = new SqlConnection("Data Source=LENOVO-PC\\SQLEXPRESS;Initial Catalog=Projectaa_Db;Integrated Security=True");

        public List<WorkItem> GetAll()
        {
            return _dbConnection.Query<WorkItem>("SELECT * FROM WorkItem").ToList();
        }

        public WorkItem Find(int id)
        {
            return _dbConnection.Query<WorkItem>("SELECT * FROM WorkItem " +
                                                "WHERE Id = @Id", new { id }).Single();
        }

        public WorkItem Add(WorkItem workItem)
        {
            var sqlQuery = "INSERT INTO WorkItem (Title, Description, DateCreated, Status_Id) " +
                           "VALUES (@Title, @Description," +  DateTime.Now + "," + 1 + ")" +
                           "SELECT Id FROM WorkItem WHERE Id = scope_identity()";
            if (workItem == null) return null;
            var workitemId = _dbConnection.Query(sqlQuery, new {workItem.Title, workItem.Description}).Single();
            workItem.Id = workitemId;
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
