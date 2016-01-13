﻿using System;
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
    public class WorkItemRepository : IWorkItemRepository
    {
        private IDbConnection _dbConnection = new SqlConnection("Server=tcp:projectaa.database.windows.net,1433;Database=projactaa_db;User ID=andreas.dellrud@projectaa;Password=TeAnAn2016;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

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
                           "VALUES (@Title, @Description, @DateCreated, @StatusId)";
            var workitemId = _dbConnection.Query(sqlQuery, workItem).Single();
            workItem.Id = workitemId;
            return workItem;
        }

        public WorkItem Update(WorkItem workItem)
        {
            var sqlQuery = "UPDATE WorkItem " +
                           "SET " +
                           "Title = @Title," +
                           "Description = @Description" +
                           "Status_Id = @StatusId" +
                           "Issue_Id = @IssueId" +
                           "Team_Id = @TeamId" +
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
                    "WHERE Description LIKE @text", new { text })
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

        public List<WorkItem> FindByStatus(Status status)
        {
            var sqlQuery = "SELECT * FROM WORKITEM" +
                           "WHERE Status_Id = @StatusId";
            return _dbConnection.Query<WorkItem>(sqlQuery, new {status.Id}).ToList();
        }
    }
}
